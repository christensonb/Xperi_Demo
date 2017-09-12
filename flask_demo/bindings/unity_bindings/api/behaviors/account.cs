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
	
	public class AccountArrayGet: ApiBehavior<AccountArrayGet>
	{
		public operations.AccountArrayGet operation;
		private int count;   			// count of the number of concurrent calls happening right now
		public bool DestroyOnComplete = false;
	    private Action<operations.AccountArrayGet, HttpResponse>  Callback;

	    public bool? primary = false;           // bool if True will only return accounts the user is primary on
        public int? offset;                     // int of the offset to use
        public int? limit;                      // int of max number of puzzles to return

		public List<models.Account> responseData; // list of Account dict the current user has access to

		private void OnSuccess(operations.AccountArrayGet operation, HttpResponse response)
		{
			responseData = operation.responseData;
			Status = ApiBehaviorStatus.SUCCESS;
		}

		private void OnFail(operations.AccountArrayGet operation, HttpResponse response)
		{
			responseData = null;
			Status = ApiBehaviorStatus.FAILURE;
		}

		private void OnComplete(operations.AccountArrayGet operation, HttpResponse response)
		{
			this.operation = operation;
			Action<operations.AccountArrayGet, HttpResponse> LocalCallback = Callback;
			OnCompletion(operation, response, BenChristensonApiMonitor.Instance);    // this will free up the behavior to accept another call
			count -= 1;
			LocalCallback(operation, response);   // this is the project's custom OnComplete
			if (DestroyOnComplete){
				this.Destroy();
			}
		}

		public void Spawn(bool? primary, int? offset, int? limit, Action<operations.AccountArrayGet, HttpResponse> Callback)
		{ // this will spawn a thread to handle the rest call and return immediately
			count += 1;
			this.Callback = Callback;
			this.primary = primary;
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

		public List<models.Account> Run(bool? primary, int? offset, int? limit)
		{ // this will block until complete
			count += 1;
			this.primary = primary;
            this.offset = offset;
            this.limit = limit;
			ExecutableCode();
			return responseData;
		}

		public override void ExecutableCode()
		{
			new operations.AccountArrayGet().SetParameters(primary, offset, limit).Send(OnSuccess, OnFail, OnComplete);
		}

        public void Destroy()
        {
            if (count == 0) {
                Destroy(this);
            }
        }
	}

	
	public class AccountGet: ApiBehavior<AccountGet>
	{
		public operations.AccountGet operation;
		private int count;   			// count of the number of concurrent calls happening right now
		public bool DestroyOnComplete = false;
	    private Action<operations.AccountGet, HttpResponse>  Callback;

	    public int account_id;                  // int of the account to get

		public models.Account responseData;     // Account dict of the account

		private void OnSuccess(operations.AccountGet operation, HttpResponse response)
		{
			responseData = operation.responseData;
			Status = ApiBehaviorStatus.SUCCESS;
		}

		private void OnFail(operations.AccountGet operation, HttpResponse response)
		{
			responseData = null;
			Status = ApiBehaviorStatus.FAILURE;
		}

		private void OnComplete(operations.AccountGet operation, HttpResponse response)
		{
			this.operation = operation;
			Action<operations.AccountGet, HttpResponse> LocalCallback = Callback;
			OnCompletion(operation, response, BenChristensonApiMonitor.Instance);    // this will free up the behavior to accept another call
			count -= 1;
			LocalCallback(operation, response);   // this is the project's custom OnComplete
			if (DestroyOnComplete){
				this.Destroy();
			}
		}

		public void Spawn(int account_id, Action<operations.AccountGet, HttpResponse> Callback)
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

		public models.Account Run(int account_id)
		{ // this will block until complete
			count += 1;
			this.account_id = account_id;
			ExecutableCode();
			return responseData;
		}

		public override void ExecutableCode()
		{
			new operations.AccountGet().SetParameters(account_id).Send(OnSuccess, OnFail, OnComplete);
		}

        public void Destroy()
        {
            if (count == 0) {
                Destroy(this);
            }
        }
	}

	
	public class AccountPut: ApiBehavior<AccountPut>
	{
		public operations.AccountPut operation;
		private int count;   			// count of the number of concurrent calls happening right now
		public bool DestroyOnComplete = false;
	    private Action<operations.AccountPut, HttpResponse>  Callback;

	    public string name;                     // str of name for the account, defaults to the created timestamp
        public List<int> user_ids;              // list of int of users to give access to this account defaults to current user

		public models.Account responseData;     // Account dict created

		private void OnSuccess(operations.AccountPut operation, HttpResponse response)
		{
			responseData = operation.responseData;
			Status = ApiBehaviorStatus.SUCCESS;
		}

		private void OnFail(operations.AccountPut operation, HttpResponse response)
		{
			responseData = null;
			Status = ApiBehaviorStatus.FAILURE;
		}

		private void OnComplete(operations.AccountPut operation, HttpResponse response)
		{
			this.operation = operation;
			Action<operations.AccountPut, HttpResponse> LocalCallback = Callback;
			OnCompletion(operation, response, BenChristensonApiMonitor.Instance);    // this will free up the behavior to accept another call
			count -= 1;
			LocalCallback(operation, response);   // this is the project's custom OnComplete
			if (DestroyOnComplete){
				this.Destroy();
			}
		}

		public void Spawn(string name, List<int> user_ids, Action<operations.AccountPut, HttpResponse> Callback)
		{ // this will spawn a thread to handle the rest call and return immediately
			count += 1;
			this.Callback = Callback;
			this.name = name;
            this.user_ids = user_ids;

 			StartCoroutine(this.ExecuteAndWait());

			/*
			if (hg.ApiWebKit.Configuration.GetSetting<bool?>("Wait_Before_Returning") == true){
                while(!this._completed){
			        new WaitForSeconds(1);
			    }
			}*/
		}

		public models.Account Run(string name, List<int> user_ids)
		{ // this will block until complete
			count += 1;
			this.name = name;
            this.user_ids = user_ids;
			ExecutableCode();
			return responseData;
		}

		public override void ExecutableCode()
		{
			new operations.AccountPut().SetParameters(name, user_ids).Send(OnSuccess, OnFail, OnComplete);
		}

        public void Destroy()
        {
            if (count == 0) {
                Destroy(this);
            }
        }
	}

	
}
