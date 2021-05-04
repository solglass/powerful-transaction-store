using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        /// <param name="transaction">Data about the extracted entity</param>
        /// <returns>Returns Id of added deposite</returns>
        // https://localhost:44365/api/dw/transaction
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("deposite")]
        public ActionResult<int> AddDeposite([FromBody] SimpleTransactionInputModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return Conflict();
            }
            var transactionDto = _mapper.Map<SimpleTransactionDto>(transaction);
            var transactionId = _transactionService.AddDeposite(transactionDto);
            return Ok(transactionId);
        }
        /// <summary>
        /// Add Withdraw
        /// </summary>
        /// <param name="transaction">Data about the extracted entity</param>
        /// <returns>Returns Id of added withdraw</returns>
        // https://localhost:44365/api/dw/transaction
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("withdraw")]
        public ActionResult<int> AddWithdraw([FromBody] SimpleTransactionInputModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return Conflict();
            }
            var transactionDto = _mapper.Map<SimpleTransactionDto>(transaction);
            var transactionId = _transactionService.AddWithdraw(transactionDto);
            return Ok(transactionId);
        }
        /// <summary>
        /// Add Transfer
        /// </summary>
        /// <param name="transfer">Data about the extracted entity</param>
        /// <returns>Returns Ids of added transfers</returns>
        // https://localhost:44365/api//tr/transaction
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("transfer")]
        public ActionResult<string> AddTransfer([FromBody] TransferInputModel transfer)
        {
            if (!ModelState.IsValid)
            {
                return Conflict();
            }
            var transferDto = _mapper.Map<TransferDto>(transfer);
            var transferIds = _transactionService.AddTransfer(transferDto);
            string serialized = JsonConvert.SerializeObject(transferIds, Formatting.Indented);
            return Ok(serialized);
        }

        /// <summary>
        /// Get list of transactions by AccountId
        /// </summary>
        /// <param name="accountId">Id of lead</param>
        /// <returns>Returns list of TransactionOutputModels</returns>
        // https://localhost:44365/api/transaction/42
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{accountId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetTransactionsByAccountId(int accountId)
        {
            var result = _mapper.Map<List<BaseTransactionOutputModel>>(_transactionService.GetTransactionsByAccountId(accountId));
            string serialized = JsonConvert.SerializeObject(result, Formatting.Indented);

            return Ok(serialized);
        }


        /// <summary>
        /// Get balance of lead
        /// </summary>
        /// <param name="inputModel">Accounts of lead</param>
        /// <returns>balance</returns>
        // https://localhost:44365/api/transaction/42
        [ProducesResponseType(typeof(WholeBalanceOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("balance")]
        public ActionResult<WholeBalanceOutputModel> GetBalance([FromBody] AccountBalanceInputModel inputModel)
        {
            var balance = _transactionService.GetBalance(inputModel.AccountIds, inputModel.Currency);
            var result = _mapper.Map<WholeBalanceOutputModel>(balance);
            return Ok(result);
        }
    }
}
