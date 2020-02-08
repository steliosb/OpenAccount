using AutoMapper;
using Service.OpenAccount.Customers.Core.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.OpenAccount.Customers.Core
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
                cfg.CreateMap<Data.Abstractions.Models.Customer, Customer>();
                cfg.CreateMap<Data.Abstractions.Models.Customer, CustomerDetail>()
                    .ForMember(dest => dest.Accounts, src => src.Ignore())
                    .ForMember(dest => dest.Balance, src => src.Ignore());
                cfg.CreateMap<Customer, Data.Abstractions.Models.Customer>();

                cfg.CreateMap<Integration.Abstractions.Models.Transaction, Transaction>();
                cfg.CreateMap<Integration.Abstractions.Models.AccountDetail, AccountDetail>()
                    .ForMember(dest=>dest.Transactions, mp=>mp.MapFrom(src=>src.Transactions.Select(t=>_mapper.Map<Integration.Abstractions.Models.Transaction>(t)).ToList()));
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
