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

namespace demo.BenChristenson
{
	public class  BenChristenson : MonoBehaviour
	{
	    public behaviors.AccountTransferWithdrawPut accountTransferWithdrawPut;
        public behaviors.AccountTransferDepositPut accountTransferDepositPut;
        public behaviors.AccountTransferArrayGet accountTransferArrayGet;
        public behaviors.AccountTransferGet accountTransferGet;
        public behaviors.AccountTransferPut accountTransferPut;
        public behaviors.AccountAccessArrayGet accountAccessArrayGet;
        public behaviors.AccountAccessPut accountAccessPut;
        public behaviors.AccountAccessDelete accountAccessDelete;
        public behaviors.AccountArrayGet accountArrayGet;
        public behaviors.AccountGet accountGet;
        public behaviors.AccountPut accountPut;
        public behaviors.EchoMessageGet echoMessageGet;
        public behaviors.EchoGet echoGet;
        public behaviors.UserLoginEmailPost userLoginEmailPost;
        public behaviors.UserLogoutPost userLogoutPost;
        public behaviors.UserSignupPut userSignupPut;
        public behaviors.UserLoginPost userLoginPost;
        public behaviors.UserGet userGet;
        public behaviors.UserPost userPost;

		public virtual void Awake()
		{
            BenChristensonApiInitialize benChristensonApiInitialize = GetComponent<BenChristensonApiInitialize>();
            if (benChristensonApiInitialize.is_behaviors_created_at_startup())
            {
			    accountTransferWithdrawPut = gameObject.AddComponent<behaviors.AccountTransferWithdrawPut>();
                accountTransferDepositPut = gameObject.AddComponent<behaviors.AccountTransferDepositPut>();
                accountTransferArrayGet = gameObject.AddComponent<behaviors.AccountTransferArrayGet>();
                accountTransferGet = gameObject.AddComponent<behaviors.AccountTransferGet>();
                accountTransferPut = gameObject.AddComponent<behaviors.AccountTransferPut>();
                accountAccessArrayGet = gameObject.AddComponent<behaviors.AccountAccessArrayGet>();
                accountAccessPut = gameObject.AddComponent<behaviors.AccountAccessPut>();
                accountAccessDelete = gameObject.AddComponent<behaviors.AccountAccessDelete>();
                accountArrayGet = gameObject.AddComponent<behaviors.AccountArrayGet>();
                accountGet = gameObject.AddComponent<behaviors.AccountGet>();
                accountPut = gameObject.AddComponent<behaviors.AccountPut>();
                echoMessageGet = gameObject.AddComponent<behaviors.EchoMessageGet>();
                echoGet = gameObject.AddComponent<behaviors.EchoGet>();
                userLoginEmailPost = gameObject.AddComponent<behaviors.UserLoginEmailPost>();
                userLogoutPost = gameObject.AddComponent<behaviors.UserLogoutPost>();
                userSignupPut = gameObject.AddComponent<behaviors.UserSignupPut>();
                userLoginPost = gameObject.AddComponent<behaviors.UserLoginPost>();
                userGet = gameObject.AddComponent<behaviors.UserGet>();
                userPost = gameObject.AddComponent<behaviors.UserPost>();
			}
		}
	}
}


