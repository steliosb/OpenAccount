using Moq;
using Service.OpenAccount.Customers.Core;
using Service.OpenAccount.Customers.Data.Abstractions;
using Service.OpenAccount.Customers.Data.Abstractions.Models;
using Service.OpenAccount.Customers.Integration.Abstractions;
using Service.OpenAccount.Customers.Integration.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Service.OpenAccount.Customers.Tests
{
	public class CustomerManagerTest
	{
		//Test for mapping and get detail function
		[Fact]
		public async Task TestGetDetail_Success()
		{
			//Arrange
			int customerId = 10;
			int accountId = 100;
			int transactionId = 1000;
			decimal amount = 100.55m;

			//Mock customer repo data
			Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
			customerRepository.Setup(f => f.GetById(It.IsAny<int>())).Returns(Task.FromResult(new Customer() { Id = customerId, Name = "firstname", Surname = "lastname" }));


			//Mock account service response with account details
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

		//Test for balance in customer get details
		[Theory]
		[InlineData("100.00", "150.55")]
		[InlineData("0.00", "15.00")]
		public async Task TestGetDetailSumBalance_Success(string firstTransactionAmount,string secondTransactionAmount)
		{
			//Arrange
			decimal firstAmount = Convert.ToDecimal(firstTransactionAmount);
			decimal secondAmount = Convert.ToDecimal(secondTransactionAmount);
			int customerId = 10;
			int accountId = 100;
			int firstTransactionId = 1000;
			int secondTransactionId = 50;


			//Mock customer repo data
			Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
			customerRepository.Setup(f => f.GetById(It.IsAny<int>())).Returns(Task.FromResult(new Customer() { Id = customerId, Name = "firstname", Surname = "lastname" }));


			//Mock account service response with account details
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
								Amount = firstAmount,
								Id = firstTransactionId
							},
							new Transaction()
							{
								AccountId = accountId,
								Amount = secondAmount,
								Id = secondTransactionId
							}
						}
					}
				}));

			CustomerManager customerManager = new CustomerManager(customerRepository.Object, accountClient.Object);

			//Act
			var result = await customerManager.GetDetail(customerId).ConfigureAwait(false);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(result.Balance, firstAmount + secondAmount);
		}

		//Integration test about fail account service
		[Fact]
		public async Task TestGetDetail_IntegrationException()
		{
			//Arrange
			int customerId = 10;

			//Mock customer repo data
			Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
			customerRepository.Setup(f => f.GetById(It.IsAny<int>())).Returns(Task.FromResult(new Customer() { Id = customerId, Name = "firstname", Surname = "lastname" }));

			//Mock account service response
			Mock<IAccountServiceClient> accountClient = new Mock<IAccountServiceClient>(MockBehavior.Strict);
			accountClient.Setup(f => f.GetByCustomerId(It.IsAny<int>())).Throws(new Exception("Test exception"));

			CustomerManager customerManager = new CustomerManager(customerRepository.Object, accountClient.Object);

			//Act
			//Assert
			await Assert.ThrowsAsync<Exception>(async () => await customerManager.GetDetail(customerId).ConfigureAwait(false)).ConfigureAwait(false);
		}
	}
}
