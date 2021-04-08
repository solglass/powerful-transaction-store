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
        /// <returns>Returns TransactionOutputModel</returns>
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
        /// <returns>Returns TransactionOutputModel</returns>
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
        /// <returns>Returns TransactionOutputModel</returns>
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
        /// Get list of transactions by leadId
        /// </summary>
        /// <param name="leadId">Id of lead</param>
        /// <returns>Returns list of TransactionOutputModels</returns>
        // https://localhost:44365/api/transaction/42
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{leadId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetTransactionsByLeadId(int leadId)
        {
            var result = _mapper.Map<List<BaseTransactionOutputModel>>(_transactionService.GetTransactionsByLeadId(leadId));
            string serialized = JsonConvert.SerializeObject(result, Formatting.Indented);

            return Ok(serialized);
        }


        /// <summary>
        /// Get balance of lead
        /// </summary>
        /// <param name="leadId">Id of lead</param>
        /// <returns>balance in decimal</returns>
        // https://localhost:44365/api/transaction/42
        [ProducesResponseType(typeof(List<LeadBalanceOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("balance/{leadId}")]
        public ActionResult<List<LeadBalanceOutputModel>> GetBalanceByLeadId(int leadId)
        {
            var balance = _transactionService.GetBalanceByLeadId(leadId);
            var result = _mapper.Map<List<LeadBalanceOutputModel>>(balance);
            return Ok(result);
        }
    }
}
