using Moq;
using Service.OpenAccount.Accounts.Core;
using Service.OpenAccount.Accounts.Data.Abstrsactions;
using Service.OpenAccount.Accounts.Data.Abstrsactions.Dto;
using Service.OpenAccount.Accounts.Integration.Abstractions;
using Service.OpenAccount.Accounts.Integration.Abstractions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Service.OpenAccount.Accounts.Tests
{
	public class AccountManagerTest
	{
		//Test for mapping and get detail function
		[Fact]
		public async Task TestGetDetail_Success()
		{
			//Arrange
			int customerId = 1;
			int accountId = 2;
			int transactionId = 50;
			decimal amount = 100.55m;

			//Mock account repo data
			Mock<IAccountRepository> accountRepository = new Mock<IAccountRepository>(MockBehavior.Strict);
			accountRepository.Setup(f => f.GetByCustomerId(It.IsAny<int>())).Returns(Task.FromResult(
				(IEnumerable<AccountDto>)new List<AccountDto>(){
					new AccountDto()
					{
						CustomerId = customerId,
						Id = accountId
					}
				}));


			//Mock transaction service response with transaction details
			Mock<ITransactionServiceClient> transactionClient = new Mock<ITransactionServiceClient>(MockBehavior.Strict);
			transactionClient.Setup(f => f.GetByAccountIds(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult(
				(IEnumerable<Transaction>)new List<Transaction>()
				{
					new Transaction()
					{
						Id = transactionId,
						AccountId = accountId,
						Amount = amount
					}
				}));

			AccountManager accountManager = new AccountManager(accountRepository.Object, transactionClient.Object,null);

			//Act
			var result = await accountManager.GetDetail(customerId).ConfigureAwait(false);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(transactionId, result.Select(r=>r.Transactions.Select(t=>t.Id).FirstOrDefault()).FirstOrDefault());
			Assert.Equal(accountId, result.Select(r => r.Id).FirstOrDefault());
			Assert.Equal(amount, result.Select(r=>r.Transactions.Select(t=>t.Amount).FirstOrDefault()).FirstOrDefault());

		}

	}
}
