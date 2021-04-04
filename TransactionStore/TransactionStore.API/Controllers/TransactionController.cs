using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.API.Models.InputModels;
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
        /// Add DepositeOrWithdraw
        /// </summary>
        /// <param name="transaction">Data about the extracted entity</param>
        /// <returns>Returns TransactionOutputModel</returns>
        // https://localhost:44365/api/dw/transaction
        [ProducesResponseType(typeof(TransactionOutputModel), StatusCodes.Status200OK)]
        [HttpPost("/depositeorwithdraw/")]
        public ActionResult<TransactionOutputModel> AddDepositeOrWithdraw([FromBody] TransactionInputModel transaction)
        {
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            _transactionService.AddDepositeOrWithdraw(transactionDto);
            var result = _mapper.Map<List<TransactionOutputModel>>(_transactionService.GetTransactionsByLeadId(transactionDto.LeadId));
            return Ok(result);
        }
        /// <summary>
        /// Add Transfer
        /// </summary>
        /// <param name="transfer">Data about the extracted entity</param>
        /// <returns>Returns TransactionOutputModel</returns>
        // https://localhost:44365/api//tr/transaction
        [ProducesResponseType(typeof(TransactionOutputModel), StatusCodes.Status200OK)]
        [HttpPost("/transfer/")]
        public ActionResult<TransferOutputModel> AddTransfer([FromBody] TransferInputModel transfer)
        {
            var transferDto = _mapper.Map<TransferDto>(transfer);
            _transactionService.AddTransfer(transferDto);
            var result = _mapper.Map<List<TransactionOutputModel>>(_transactionService.GetTransfersByLeadId(transferDto.LeadId));
            return Ok(result);
        }

        /// <summary>
        /// Get list of transactions by leadId
        /// </summary>
        /// <param name="leadId">Id of lead</param>
        /// <returns>Returns list of TransactionOutputModels</returns>
        // https://localhost:44365/api/transaction/42
        [ProducesResponseType(typeof(List<TransactionOutputModel>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("/transactions/{leadId}")]
        public ActionResult<List<TransactionOutputModel>> GetTransactionsByLeadId(int leadId)
        {
            var transactionDto = _transactionService.GetTransactionsByLeadId(leadId);
            var result = _mapper.Map<List<TransactionOutputModel>>(transactionDto);
            return Ok(result);
        }
        /// <summary>
        /// Get list of transfers by leadId
        /// </summary>
        /// <param name="leadId">Id of lead</param>
        /// <returns>Returns list of TransferOutputModels</returns>
        // https://localhost:44365/api/transaction/42
        [ProducesResponseType(typeof(List<TransferOutputModel>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("/transfers/{leadId}")]
        public ActionResult<List<TransferOutputModel>> GetTransfersByLeadId(int leadId)
        {
            var transferDto = _transactionService.GetTransfersByLeadId(leadId);
            var result = _mapper.Map<List<TransferOutputModel>>(transferDto);
            return Ok(result);
        }

        /// <summary>
        /// Get balance of lead
        /// </summary>
        /// <param name="leadId">Id of lead</param>
        /// <returns>balance in decimal</returns>
        // https://localhost:44365/api/transaction/42
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("/balance/{leadId}")]
        public ActionResult<decimal> GetBalanceByLeadId(int leadId)
        {
            var balance = _transactionService.GetBalanceByLeadId(leadId);
            return Ok(balance);
        }
    }
}
