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
	[HttpPath("BenChristenson","account/array")]
	public class AccountArrayGet: HttpOperation
	{
	    [HttpQueryString] public int? offset;                     // int of the offset to use
        [HttpQueryString] public int? limit;                      // int of max number of puzzles to return

		[HttpResponseJsonBody]
		public List<models.Account> responseData; // list of Account dict the current user has access to

		public AccountArrayGet SetParameters(int? offset, int? limit)
		{
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
	[HttpPath("BenChristenson","account")]
	public class AccountGet: HttpOperation
	{
	    [HttpQueryString] public int account_id;                  // int of the account to get

		[HttpResponseJsonBody]
		public models.Account responseData;     // Account dict of the account

		public AccountGet SetParameters(int account_id)
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
	[HttpPath("BenChristenson","account/put")]
	public class AccountPut: HttpOperation
	{
	    [HttpFormField] public string name;                     // str of name for the account, defaults to the created timestamp
        [HttpFormField] public List<int> user_ids;              // list of int of users to give access to this account defaults to current user

		[HttpResponseJsonBody]
		public models.Account responseData;     // Account dict created

		public AccountPut SetParameters(string name, List<int> user_ids)
		{
		    this.name = name;
            this.user_ids = user_ids;
			return this;
		}

		protected override void FromResponse(HttpResponse response)
		{
			base.FromResponse(response);
		}
	}

	
}
