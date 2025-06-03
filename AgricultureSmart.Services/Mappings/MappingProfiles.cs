using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Services.Models.AssignmentModel;
using AgricultureSmart.Services.Models.CartModels;
using AgricultureSmart.Services.Models.EngineerModel;
using AgricultureSmart.Services.Models.FarmerModels;
using AgricultureSmart.Services.Models.ProductCategoryModels;
using AgricultureSmart.Services.Models.ProductModels;
using AgricultureSmart.Services.Models.TicketModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Ticket, TicketViewModel>().ReverseMap();
            CreateMap<CreateTicketModel, Ticket>().ReverseMap();
            CreateMap<UpdateTicketModel, Ticket>().ReverseMap();

            CreateMap<Farmer, FarmerViewModel>().ReverseMap();
            CreateMap<CreateFarmerModel, Farmer>().ReverseMap();
            CreateMap<UpdateFarmerModel, Farmer>().ReverseMap();

            CreateMap<Engineer, EngineerViewModel>().ReverseMap();
            CreateMap<CreateEngineerModel, Engineer>().ReverseMap();
            CreateMap<UpdateEngineerModel, Engineer>().ReverseMap();

            CreateMap<EngineerFarmerAssignment, EngineerFarmerAssignmentViewModel>().ReverseMap();
            CreateMap<CreateAssignmentModel, EngineerFarmerAssignment>().ReverseMap();
            CreateMap<UpdateAssignmentModel, EngineerFarmerAssignment>().ReverseMap();

            // ProductCategory mappings
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<CreateProductCategoryDto, ProductCategory>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateProductCategoryDto, ProductCategory>();

            // Product mappings
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
                
            // Cart mappings
            CreateMap<Cart, CartDto>();
            
            // CartItem mappings
            CreateMap<CartItem, CartItemDto>();
            CreateMap<AddToCartDto, CartItem>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
