using AutoMapper;
using Service.OpenAccount.Transactions.Core.Abstractions.Models;
using Service.OpenAccount.Transactions.Data.Abstarctions.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Transactions.Core
{
    class MappingConfiguration
    {
        public MappingConfiguration() { }

        private static IMapper _mapper;
        public IMapper GetConfigureMapper()
        {
            try
            {
                if (_mapper == null) _mapper = getConfigureMapper();
                return _mapper;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static IMapper getConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Mapping from TransactionDto to Transaction
                cfg.CreateMap<TransactionDto, Transaction>();

                //Mapping from Transaction to TransactionDto
                cfg.CreateMap<Transaction, TransactionDto>();
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
