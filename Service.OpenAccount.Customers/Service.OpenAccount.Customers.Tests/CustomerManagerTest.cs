using Moq;
using Service.OpenAccount.Customers.Core;
using Service.OpenAccount.Customers.Data.Abstractions;
using Service.OpenAccount.Customers.Data.Abstractions.Models;
using Service.OpenAccount.Customers.Integration.Abstractions;
using Service.OpenAccount.Customers.Integration.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Service.OpenAccount.Customers.Tests
{
	public class CustomerManagerTest
	{
		[Fact]
		public async Task TestGetDetail_Success()
		{
			//Arrange
			int customerId = 10;
			int accountId = 100;
			int transactionId = 1000;
			decimal amount = 100.55m;

			Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
			customerRepository.Setup(f => f.GetById(It.IsAny<int>())).Returns(Task.FromResult(new Customer() { Id = customerId, Name = "firstname", Surname = "lastname" }));
			
			Mock<IAccountServiceClient> accountClient = new Mock<IAccountServiceClient>(MockBehavior.Strict);
			accountClient.Setup(f => f.GetByCustomerId(It.IsAny<int>())).Returns(Task.FromResult(
				(IEnumerable<AccountDetail>)new List<AccountDetail>()
				{
					new AccountDetail()
					{
						CustomerId = customerId,
						Id = accountId,
						Transactions = new List<Transaction>()
						{
							new Transaction() 
							{ 
								AccountId = accountId,
								Amount = amount,
								Id = transactionId
							}
						}
					}
				}));

			CustomerManager customerManager = new CustomerManager(customerRepository.Object, accountClient.Object);

			//Act
			var result = await customerManager.GetDetail(customerId).ConfigureAwait(false);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(customerId, result.Id);
			Assert.Equal(amount, result.Balance);
			Assert.Equal(accountId, result.Accounts.First().Id);
			Assert.Equal(transactionId, result.Accounts.First().Transactions.First().Id);

		}

		[Fact]
		public async Task TestGetDetail_IntegrationException()
		{
			//Arrange
			int customerId = 10;

			Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
			customerRepository.Setup(f => f.GetById(It.IsAny<int>())).Returns(Task.FromResult(new Customer() { Id = customerId, Name = "firstname", Surname = "lastname" }));

			Mock<IAccountServiceClient> accountClient = new Mock<IAccountServiceClient>(MockBehavior.Strict);
			accountClient.Setup(f => f.GetByCustomerId(It.IsAny<int>())).Throws(new Exception("Test exception"));

			CustomerManager customerManager = new CustomerManager(customerRepository.Object, accountClient.Object);

			//Act
			//Assert
			await Assert.ThrowsAsync<Exception>(async () => await customerManager.GetDetail(customerId).ConfigureAwait(false)).ConfigureAwait(false);
		}
	}
}
