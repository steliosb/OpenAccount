using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Service.OpenAccount.Transactions.WebApi
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
                cfg.CreateMap<WebApi.Models.Transaction, Core.Abstractions.Models.Transaction>();
                cfg.CreateMap<Core.Abstractions.Models.Transaction, WebApi.Models.Transaction>();
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
