using AutoMapper;
using BatchSystem.Application.Queries.ProductionOrder.DTO;
using Domain.OrderBatchs;
using Domain.ProductionOrders;


namespace BatchSystem.Mapping
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<ProductionOrder, ProductionOrderReport>()
                 .ForMember(dest => dest.ProductionOrderId,
                    opt => opt.MapFrom(src => src.ProductionOrderId))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.PlannedStartTime,
                    opt => opt.MapFrom(src => src.PlannedStartTime))
                .ForMember(dest => dest.PlannedEndTime,
                    opt => opt.MapFrom(src => src.PlannedEndTime))
                .ForMember(dest => dest.ActualStartTime,
                    opt => opt.MapFrom(src => src.ActualStartTime))
                .ForMember(dest => dest.ActualEndTime,
                    opt => opt.MapFrom(src => src.ActualEndTime))

                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.CustomerLogin != null ? src.CustomerLogin.UserName : null))
                .ForMember(dest => dest.Phone,
                    opt => opt.MapFrom(src => src.CustomerLogin != null ? src.CustomerLogin.PhoneNumber.ToString() : null))
                .ForMember(dest => dest.OrderDay,
                    opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Details,
                    opt => opt.MapFrom(src => src.ProductionOrderDetails.OrderBy(d => d.SequenceNo)));


            CreateMap<ProductionOrderDetail, ProductionOrderReportDetail>()
                 .ForMember(dest => dest.ProductionOrderDetailId,
                    opt => opt.MapFrom(src => src.ProductionOrderDetailId))
                .ForMember(dest => dest.SequenceNo,
                    opt => opt.MapFrom(src => src.SequenceNo))

                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null))
                .ForMember(dest => dest.TargetBatch,
                    opt => opt.MapFrom(src => src.NumberOfPieces))
                .ForMember(dest => dest.CurrentBatch,
                    opt => opt.MapFrom(src => src.OrderBatches.Count(b => b.Status == OrderBatchStatus.Completed)))
                .ForMember(dest => dest.RecipeSnapshot,
                    opt => opt.MapFrom(src => src.GetRecipeSnapshot()));
        }
    }
}
