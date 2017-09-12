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
	
	[HttpGET]
	[HttpPath("BenChristenson","account/access/array")]
	public class AccountAccessArrayGet: HttpOperation
	{
	    [HttpQueryString] public int account_id;                  // int of the account_id for the account

		[HttpResponseJsonBody]
		public List<models.User> responseData;  // list of User dict

		public AccountAccessArrayGet SetParameters(int account_id)
		{
		    this.account_id = account_id;
			return this;
		}

		protected override void FromResponse(HttpResponse response)
		{
			base.FromResponse(response);
		}
	}

	
	[HttpPOST]
	[HttpPath("BenChristenson","account/access/put")]
	public class AccountAccessPut: HttpOperation
	{
	    [HttpFormField] public int account_id;                  // int of the account_id for the account
        [HttpFormField] public int user_id;                     // int of the user_id to grant access

		[HttpResponseJsonBody]
		public models.Access responseData;      // Access dict

		public AccountAccessPut SetParameters(int account_id, int user_id)
		{
		    this.account_id = account_id;
            this.user_id = user_id;
			return this;
		}

		protected override void FromResponse(HttpResponse response)
		{
			base.FromResponse(response);
		}
	}

	
	[HttpDELETE]
	[HttpPath("BenChristenson","account/access")]
	public class AccountAccessDelete: HttpOperation
	{
	    [HttpFormField] public int account_id;                  // int of the account_id for the account
        [HttpFormField] public int user_id;                     // int of the user_id to grant access

		[HttpResponseJsonBody]
		public models.Access responseData;      // Access dict

		public AccountAccessDelete SetParameters(int account_id, int user_id)
		{
		    this.account_id = account_id;
            this.user_id = user_id;
			return this;
		}

		protected override void FromResponse(HttpResponse response)
		{
			base.FromResponse(response);
		}
	}

	
}
