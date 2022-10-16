using AutoMapper;
using ClientesAPI.Models;
using ClientesAPI.Models.Dto;

namespace ClientesAPI {
    public class MappingConfig {
        public static MapperConfiguration RegisterMaps() {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<ClienteDto, Cliente>();
                config.CreateMap<Cliente, ClienteDto>();
            });
            return mappingConfig;
        }
    }
}
