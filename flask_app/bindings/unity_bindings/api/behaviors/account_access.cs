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
	
	public class AccountAccessArrayGet: ApiBehavior<AccountAccessArrayGet>
	{
		public operations.AccountAccessArrayGet operation;
		private int count;   			// count of the number of concurrent calls happening right now
		public bool DestroyOnComplete = false;
	    private Action<operations.AccountAccessArrayGet, HttpResponse>  Callback;

	    public int account_id;                  // int of the account_id for the account

		public List<models.User> responseData;  // list of User dict

		private void OnSuccess(operations.AccountAccessArrayGet operation, HttpResponse response)
		{
			responseData = operation.responseData;
			Status = ApiBehaviorStatus.SUCCESS;
		}

		private void OnFail(operations.AccountAccessArrayGet operation, HttpResponse response)
		{
			responseData = null;
			Status = ApiBehaviorStatus.FAILURE;
		}

		private void OnComplete(operations.AccountAccessArrayGet operation, HttpResponse response)
		{
			this.operation = operation;
			Action<operations.AccountAccessArrayGet, HttpResponse> LocalCallback = Callback;
			OnCompletion(operation, response, BenChristensonApiMonitor.Instance);    // this will free up the behavior to accept another call
			count -= 1;
			LocalCallback(operation, response);   // this is the project's custom OnComplete
			if (DestroyOnComplete){
				this.Destroy();
			}
		}

		public void Spawn(int account_id, Action<operations.AccountAccessArrayGet, HttpResponse> Callback)
		{ // this will spawn a thread to handle the rest call and return immediately
			count += 1;
			this.Callback = Callback;
			this.account_id = account_id;

 			StartCoroutine(this.ExecuteAndWait());

			/*
			if (hg.ApiWebKit.Configuration.GetSetting<bool?>("Wait_Before_Returning") == true){
                while(!this._completed){
			        new WaitForSeconds(1);
			    }
			}*/
		}

		public List<models.User> Run(int account_id)
		{ // this will block until complete
			count += 1;
			this.account_id = account_id;
			ExecutableCode();
			return responseData;
		}

		public override void ExecutableCode()
		{
			new operations.AccountAccessArrayGet().SetParameters(account_id).Send(OnSuccess, OnFail, OnComplete);
		}

        public void Destroy()
        {
            if (count == 0) {
                Destroy(this);
            }
        }
	}

	
	public class AccountAccessPut: ApiBehavior<AccountAccessPut>
	{
		public operations.AccountAccessPut operation;
		private int count;   			// count of the number of concurrent calls happening right now
		public bool DestroyOnComplete = false;
	    private Action<operations.AccountAccessPut, HttpResponse>  Callback;

	    public int account_id;                  // int of the account_id for the account
        public int user_id;                     // int of the user_id to grant access

		public models.Access responseData;      // Access dict

		private void OnSuccess(operations.AccountAccessPut operation, HttpResponse response)
		{
			responseData = operation.responseData;
			Status = ApiBehaviorStatus.SUCCESS;
		}

		private void OnFail(operations.AccountAccessPut operation, HttpResponse response)
		{
			responseData = null;
			Status = ApiBehaviorStatus.FAILURE;
		}

		private void OnComplete(operations.AccountAccessPut operation, HttpResponse response)
		{
			this.operation = operation;
			Action<operations.AccountAccessPut, HttpResponse> LocalCallback = Callback;
			OnCompletion(operation, response, BenChristensonApiMonitor.Instance);    // this will free up the behavior to accept another call
			count -= 1;
			LocalCallback(operation, response);   // this is the project's custom OnComplete
			if (DestroyOnComplete){
				this.Destroy();
			}
		}

		public void Spawn(int account_id, int user_id, Action<operations.AccountAccessPut, HttpResponse> Callback)
		{ // this will spawn a thread to handle the rest call and return immediately
			count += 1;
			this.Callback = Callback;
			this.account_id = account_id;
            this.user_id = user_id;

 			StartCoroutine(this.ExecuteAndWait());

			/*
			if (hg.ApiWebKit.Configuration.GetSetting<bool?>("Wait_Before_Returning") == true){
                while(!this._completed){
			        new WaitForSeconds(1);
			    }
			}*/
		}

		public models.Access Run(int account_id, int user_id)
		{ // this will block until complete
			count += 1;
			this.account_id = account_id;
            this.user_id = user_id;
			ExecutableCode();
			return responseData;
		}

		public override void ExecutableCode()
		{
			new operations.AccountAccessPut().SetParameters(account_id, user_id).Send(OnSuccess, OnFail, OnComplete);
		}

        public void Destroy()
        {
            if (count == 0) {
                Destroy(this);
            }
        }
	}

	
	public class AccountAccessDelete: ApiBehavior<AccountAccessDelete>
	{
		public operations.AccountAccessDelete operation;
		private int count;   			// count of the number of concurrent calls happening right now
		public bool DestroyOnComplete = false;
	    private Action<operations.AccountAccessDelete, HttpResponse>  Callback;

	    public int account_id;                  // int of the account_id for the account
        public int user_id;                     // int of the user_id to grant access

		public models.Access responseData;      // Access dict

		private void OnSuccess(operations.AccountAccessDelete operation, HttpResponse response)
		{
			responseData = operation.responseData;
			Status = ApiBehaviorStatus.SUCCESS;
		}

		private void OnFail(operations.AccountAccessDelete operation, HttpResponse response)
		{
			responseData = null;
			Status = ApiBehaviorStatus.FAILURE;
		}

		private void OnComplete(operations.AccountAccessDelete operation, HttpResponse response)
		{
			this.operation = operation;
			Action<operations.AccountAccessDelete, HttpResponse> LocalCallback = Callback;
			OnCompletion(operation, response, BenChristensonApiMonitor.Instance);    // this will free up the behavior to accept another call
			count -= 1;
			LocalCallback(operation, response);   // this is the project's custom OnComplete
			if (DestroyOnComplete){
				this.Destroy();
			}
		}

		public void Spawn(int account_id, int user_id, Action<operations.AccountAccessDelete, HttpResponse> Callback)
		{ // this will spawn a thread to handle the rest call and return immediately
			count += 1;
			this.Callback = Callback;
			this.account_id = account_id;
            this.user_id = user_id;

 			StartCoroutine(this.ExecuteAndWait());

			/*
			if (hg.ApiWebKit.Configuration.GetSetting<bool?>("Wait_Before_Returning") == true){
                while(!this._completed){
			        new WaitForSeconds(1);
			    }
			}*/
		}

		public models.Access Run(int account_id, int user_id)
		{ // this will block until complete
			count += 1;
			this.account_id = account_id;
            this.user_id = user_id;
			ExecutableCode();
			return responseData;
		}

		public override void ExecutableCode()
		{
			new operations.AccountAccessDelete().SetParameters(account_id, user_id).Send(OnSuccess, OnFail, OnComplete);
		}

        public void Destroy()
        {
            if (count == 0) {
                Destroy(this);
            }
        }
	}

	
}
