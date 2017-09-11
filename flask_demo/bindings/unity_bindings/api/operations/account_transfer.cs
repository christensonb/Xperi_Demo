/*
    This file contains the operations code for the REST Endpoint
    It is the lowest level code before the WAK library.
*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using hg.ApiWebKit;
using hg.ApiWebKit.core.http;
using hg.ApiWebKit.faulters;
using hg.ApiWebKit.mappers;
using hg.ApiWebKit.core.attributes;
using hg.ApiWebKit.apis;

namespace demo.BenChristenson.operations
{
	
	[HttpPOST]
	[HttpPath("BenChristenson","account/transfer/withdraw/put")]
	public class AccountTransferWithdrawPut: HttpOperation
	{
	    [HttpFormField] public int withdraw_acount_id;          // int of the account_id to withdraw the money from
        [HttpFormField] public float amount;                    // float of the amount to transfer

		[HttpResponseJsonBody]
		public models.Transfer responseData;    // Transfer dict

		public AccountTransferWithdrawPut SetParameters(int withdraw_acount_id, float amount)
		{
		    this.withdraw_acount_id = withdraw_acount_id;
            this.amount = amount;
			return this;
		}

		protected override void FromResponse(HttpResponse response)
		{
			base.FromResponse(response);
		}
	}

	
	[HttpPOST]
	[HttpPath("BenChristenson","account/transfer/deposit/put")]
	public class AccountTransferDepositPut: HttpOperation
	{
	    [HttpFormField] public int deposit_account_id;          // int of the account_id to deposit the moeny to
        [HttpFormField] public float amount;                    // float of the amount to transfer
        [HttpFormField] public string deposit_receipt;          // str of the validated receipt that money has been received

		[HttpResponseJsonBody]
		public models.Transfer responseData;    // Transfer dict

		public AccountTransferDepositPut SetParameters(int deposit_account_id, float amount, string deposit_receipt)
		{
		    this.deposit_account_id = deposit_account_id;
            this.amount = amount;
            this.deposit_receipt = deposit_receipt;
			return this;
		}

		protected override void FromResponse(HttpResponse response)
		{
			base.FromResponse(response);
		}
	}

	
	[HttpGET]
	[HttpPath("BenChristenson","account/transfer/array")]
	public class AccountTransferArrayGet: HttpOperation
	{
	    [HttpQueryString] public int account_id;                  // int of the account_id to get transfer for
        [HttpQueryString] public bool? withdraws_only;            // bool if true only gets withdraw transfer if false only gets deposit, default gets both
        [HttpQueryString] public int? offset;                     // int of the offset to use
        [HttpQueryString] public int? limit;                      // int of max number of puzzles to return

		[HttpResponseJsonBody]
		public List<models.Transfer> responseData; // list of Transfer dict

		public AccountTransferArrayGet SetParameters(int account_id, bool? withdraws_only, int? offset, int? limit)
		{
		    this.account_id = account_id;
            this.withdraws_only = withdraws_only;
            this.offset = offset;
            this.limit = limit;
			return this;
		}

		protected override void FromResponse(HttpResponse response)
		{
			base.FromResponse(response);
		}
	}

	
	[HttpGET]
	[HttpPath("BenChristenson","account/transfer")]
	public class AccountTransferGet: HttpOperation
	{
	    [HttpQueryString] public int transfer_id;                 // int of the id for the transfer

		[HttpResponseJsonBody]
		public models.Transfer responseData;    // Transfer dict

		public AccountTransferGet SetParameters(int transfer_id)
		{
		    this.transfer_id = transfer_id;
			return this;
		}

		protected override void FromResponse(HttpResponse response)
		{
			base.FromResponse(response);
		}
	}

	
	[HttpPOST]
	[HttpPath("BenChristenson","account/transfer/put")]
	public class AccountTransferPut: HttpOperation
	{
	    [HttpFormField] public int withdraw_acount_id;          // int of the account_id to withdraw the money from
        [HttpFormField] public int deposit_account_id;          // int of the account_id to deposit the moeny to
        [HttpFormField] public float amount;                    // float of the amount to transfer

		[HttpResponseJsonBody]
		public models.Transfer responseData;    // Transfer dict

		public AccountTransferPut SetParameters(int withdraw_acount_id, int deposit_account_id, float amount)
		{
		    this.withdraw_acount_id = withdraw_acount_id;
            this.deposit_account_id = deposit_account_id;
            this.amount = amount;
			return this;
		}

		protected override void FromResponse(HttpResponse response)
		{
			base.FromResponse(response);
		}
	}

	
}
