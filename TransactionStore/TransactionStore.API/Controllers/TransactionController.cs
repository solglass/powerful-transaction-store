using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionStore.API.Models.InputModels;
using TransactionStore.API.Models.OutputModels;
using TransactionStore.Business;
using TransactionStore.Core.Models;

namespace TransactionStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private ITransactionService _transactionService;
        private IMapper _mapper;

        public TransactionController(IMapper mapper, ITransactionService transactionService)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Add Deposite
        /// </summary>
        /// <param name="transaction">Information about transaction</param>
        /// <returns>Returns Id of added deposite</returns>
        // https://localhost:44365/api/dw/transaction
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("deposite")]
        public async Task<ActionResult<int>> AddDeposite([FromBody] SimpleTransactionInputModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return Conflict();
            }
            var balance = await _transactionService.GetBalanceWithTimestampAsync(transaction.Account.AccountId);

            var transactionDto =  _mapper.Map<SimpleTransactionDto>(transaction);
            var transactionId =  await _transactionService.AddDepositeAsync(transactionDto, (DateTime)balance.Timestamp);
            return Ok(transactionId);
        }
        /// <summary>
        /// Add Withdraw
        /// </summary>
        /// <param name="transaction">Information about transaction</param>
        /// <returns>Returns Id of added withdraw</returns>
        // https://localhost:44365/api/dw/transaction
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("withdraw")]
        public async Task<ActionResult<int>> AddWithdraw([FromBody] SimpleTransactionInputModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return Conflict();
            }
            var balance = await _transactionService.GetBalanceWithTimestampAsync(transaction.Account.AccountId);
            var balanceInWithdrawCurrency = await _transactionService.ConvertAmount(transaction.Account.Currency, transaction.Value.Currency, balance.Amount);
            if (balanceInWithdrawCurrency < transaction.Value.Amount)
                return BadRequest("Not enough funds");

            var transactionDto = _mapper.Map<SimpleTransactionDto>(transaction);
            var transactionId = await _transactionService.AddWithdrawAsync(transactionDto, (DateTime)balance.Timestamp);
            return Ok(transactionId);
        }
        /// <summary>
        /// Add Transfer
        /// </summary>
        /// <param name="transfer">Information about transaction</param>
        /// <returns>Returns Ids of added transfers</returns>
        // https://localhost:44365/api//tr/transaction
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("transfer")]
        public async Task<ActionResult<string>> AddTransfer([FromBody] TransferInputModel transfer)
        {
            if (!ModelState.IsValid)
            {
                return Conflict(); 
            }
            var balance = await _transactionService.GetBalanceWithTimestampAsync( transfer.SenderAccount.AccountId);
            if (balance.Amount < transfer.Amount)
                return BadRequest("Not enough funds");
            
            var transferDto = _mapper.Map<TransferDto>(transfer);

            var transferIds = await _transactionService.AddTransferAsync(transferDto, (DateTime)balance.Timestamp);
            string serialized = JsonConvert.SerializeObject(transferIds, Formatting.Indented);
            return Ok(serialized);
        }

        /// <summary>
        /// Get list of all transactions by AccountIds
        /// </summary>
        /// <returns>Returns list of all Transactions by AccountIds in List</returns>
        // https://localhost:44365/api/transaction/
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> GetTransactionsListByAccountIds([FromBody] List<int> accountIds)
        {                  
            var dto = await _transactionService.GetTransactionsByAccountIdsAsync(accountIds);
            var result = _mapper.Map<List<BaseTransactionOutputModel>>(dto);
            string serialized = JsonConvert.SerializeObject(result, Formatting.Indented);
            return Ok(serialized);
        }
        /// <summary>
        /// Get balance of Accounts
        /// </summary>
        /// <param name="inputModel">list of accounts Ids and currency for whole balance</param>
        /// <returns>whole balance in certain currency</returns>
        // https://localhost:44365/api/transaction/42
        [ProducesResponseType(typeof(WholeBalanceOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("balance")]
        public async Task<ActionResult<WholeBalanceOutputModel>> GetBalance([FromBody] AccountBalanceInputModel inputModel)
        {
            var balance = await _transactionService.GetBalanceAsync(inputModel.AccountIds, inputModel.Currency);
            var result = _mapper.Map<WholeBalanceOutputModel>(balance);
            return Ok(result);
        }
    }
}
