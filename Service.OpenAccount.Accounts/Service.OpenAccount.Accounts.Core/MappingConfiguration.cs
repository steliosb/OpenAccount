using AutoMapper;
using Service.OpenAccount.Accounts.Core.Abstractions.Models;
using Service.OpenAccount.Accounts.Data.Abstrsactions.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Accounts.Core
{
    public class MappingConfiguration
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
                cfg.CreateMap<AccountDto, Account>();
                cfg.CreateMap<AccountDto, AccountDetail>().ForMember(dest => dest.Transactions, src => src.Ignore());
                cfg.CreateMap<Account, AccountDto>();

                cfg.CreateMap<Integration.Abstractions.Models.Transaction, Transaction>();
                cfg.CreateMap<Transaction, Integration.Abstractions.Models.Transaction>();
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
