/*
    This file will contains the behaviors and will automatically be attached
    by the root_namespace mono-behavior
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
using demo.BenChristenson;

namespace demo.BenChristenson.behaviors
{
	
	public class AccountTransferArrayGet: ApiBehavior<AccountTransferArrayGet>
	{
		public operations.AccountTransferArrayGet operation;
		private int count;   			// count of the number of concurrent calls happening right now
		public bool DestroyOnComplete = false;
	    private Action<operations.AccountTransferArrayGet, HttpResponse>  Callback;

	    public int account_id;                  // int of the account_id to get transfer for
        public bool? withdraws_only;            // bool if true only gets withdraw transfer if false only gets deposit, default gets both
        public int? offset;                     // int of the offset to use
        public int? limit;                      // int of max number of puzzles to return

		public List<models.Transfer> responseData; // list of Transfer dict

		private void OnSuccess(operations.AccountTransferArrayGet operation, HttpResponse response)
		{
			responseData = operation.responseData;
			Status = ApiBehaviorStatus.SUCCESS;
		}

		private void OnFail(operations.AccountTransferArrayGet operation, HttpResponse response)
		{
			responseData = null;
			Status = ApiBehaviorStatus.FAILURE;
		}

		private void OnComplete(operations.AccountTransferArrayGet operation, HttpResponse response)
		{
			this.operation = operation;
			Action<operations.AccountTransferArrayGet, HttpResponse> LocalCallback = Callback;
			OnCompletion(operation, response, BenChristensonApiMonitor.Instance);    // this will free up the behavior to accept another call
			count -= 1;
			LocalCallback(operation, response);   // this is the project's custom OnComplete
			if (DestroyOnComplete){
				this.Destroy();
			}
		}

		public void Spawn(int account_id, bool? withdraws_only, int? offset, int? limit, Action<operations.AccountTransferArrayGet, HttpResponse> Callback)
		{ // this will spawn a thread to handle the rest call and return immediately
			count += 1;
			this.Callback = Callback;
			this.account_id = account_id;
            this.withdraws_only = withdraws_only;
            this.offset = offset;
            this.limit = limit;

 			StartCoroutine(this.ExecuteAndWait());

			/*
			if (hg.ApiWebKit.Configuration.GetSetting<bool?>("Wait_Before_Returning") == true){
                while(!this._completed){
			        new WaitForSeconds(1);
			    }
			}*/
		}

		public List<models.Transfer> Run(int account_id, bool? withdraws_only, int? offset, int? limit)
		{ // this will block until complete
			count += 1;
			this.account_id = account_id;
            this.withdraws_only = withdraws_only;
            this.offset = offset;
            this.limit = limit;
			ExecutableCode();
			return responseData;
		}

		public override void ExecutableCode()
		{
			new operations.AccountTransferArrayGet().SetParameters(account_id, withdraws_only, offset, limit).Send(OnSuccess, OnFail, OnComplete);
		}

        public void Destroy()
        {
            if (count == 0) {
                Destroy(this);
            }
        }
	}

	
	public class AccountTransferGet: ApiBehavior<AccountTransferGet>
	{
		public operations.AccountTransferGet operation;
		private int count;   			// count of the number of concurrent calls happening right now
		public bool DestroyOnComplete = false;
	    private Action<operations.AccountTransferGet, HttpResponse>  Callback;

	    public int transfer_id;                 // int of the id for the transfer

		public models.Transfer responseData;    // Transfer dict

		private void OnSuccess(operations.AccountTransferGet operation, HttpResponse response)
		{
			responseData = operation.responseData;
			Status = ApiBehaviorStatus.SUCCESS;
		}

		private void OnFail(operations.AccountTransferGet operation, HttpResponse response)
		{
			responseData = null;
			Status = ApiBehaviorStatus.FAILURE;
		}

		private void OnComplete(operations.AccountTransferGet operation, HttpResponse response)
		{
			this.operation = operation;
			Action<operations.AccountTransferGet, HttpResponse> LocalCallback = Callback;
			OnCompletion(operation, response, BenChristensonApiMonitor.Instance);    // this will free up the behavior to accept another call
			count -= 1;
			LocalCallback(operation, response);   // this is the project's custom OnComplete
			if (DestroyOnComplete){
				this.Destroy();
			}
		}

		public void Spawn(int transfer_id, Action<operations.AccountTransferGet, HttpResponse> Callback)
		{ // this will spawn a thread to handle the rest call and return immediately
			count += 1;
			this.Callback = Callback;
			this.transfer_id = transfer_id;

 			StartCoroutine(this.ExecuteAndWait());

			/*
			if (hg.ApiWebKit.Configuration.GetSetting<bool?>("Wait_Before_Returning") == true){
                while(!this._completed){
			        new WaitForSeconds(1);
			    }
			}*/
		}

		public models.Transfer Run(int transfer_id)
		{ // this will block until complete
			count += 1;
			this.transfer_id = transfer_id;
			ExecutableCode();
			return responseData;
		}

		public override void ExecutableCode()
		{
			new operations.AccountTransferGet().SetParameters(transfer_id).Send(OnSuccess, OnFail, OnComplete);
		}

        public void Destroy()
        {
            if (count == 0) {
                Destroy(this);
            }
        }
	}

	
	public class AccountTransferPut: ApiBehavior<AccountTransferPut>
	{
		public operations.AccountTransferPut operation;
		private int count;   			// count of the number of concurrent calls happening right now
		public bool DestroyOnComplete = false;
	    private Action<operations.AccountTransferPut, HttpResponse>  Callback;

	    public int withdraw_acount_id;          // int of the account_id to withdraw the money from
        public int deposit_account_id;          // int of the account_id to deposit the moeny to
        public float amount;                    // float of the amount to transfer

		public models.Transfer responseData;    // Transfer dict

		private void OnSuccess(operations.AccountTransferPut operation, HttpResponse response)
		{
			responseData = operation.responseData;
			Status = ApiBehaviorStatus.SUCCESS;
		}

		private void OnFail(operations.AccountTransferPut operation, HttpResponse response)
		{
			responseData = null;
			Status = ApiBehaviorStatus.FAILURE;
		}

		private void OnComplete(operations.AccountTransferPut operation, HttpResponse response)
		{
			this.operation = operation;
			Action<operations.AccountTransferPut, HttpResponse> LocalCallback = Callback;
			OnCompletion(operation, response, BenChristensonApiMonitor.Instance);    // this will free up the behavior to accept another call
			count -= 1;
			LocalCallback(operation, response);   // this is the project's custom OnComplete
			if (DestroyOnComplete){
				this.Destroy();
			}
		}

		public void Spawn(int withdraw_acount_id, int deposit_account_id, float amount, Action<operations.AccountTransferPut, HttpResponse> Callback)
		{ // this will spawn a thread to handle the rest call and return immediately
			count += 1;
			this.Callback = Callback;
			this.withdraw_acount_id = withdraw_acount_id;
            this.deposit_account_id = deposit_account_id;
            this.amount = amount;

 			StartCoroutine(this.ExecuteAndWait());

			/*
			if (hg.ApiWebKit.Configuration.GetSetting<bool?>("Wait_Before_Returning") == true){
                while(!this._completed){
			        new WaitForSeconds(1);
			    }
			}*/
		}

		public models.Transfer Run(int withdraw_acount_id, int deposit_account_id, float amount)
		{ // this will block until complete
			count += 1;
			this.withdraw_acount_id = withdraw_acount_id;
            this.deposit_account_id = deposit_account_id;
            this.amount = amount;
			ExecutableCode();
			return responseData;
		}

		public override void ExecutableCode()
		{
			new operations.AccountTransferPut().SetParameters(withdraw_acount_id, deposit_account_id, amount).Send(OnSuccess, OnFail, OnComplete);
		}

        public void Destroy()
        {
            if (count == 0) {
                Destroy(this);
            }
        }
	}

	
}