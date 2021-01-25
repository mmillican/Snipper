using AutoMapper;
using Snipper.Web.Entities;

namespace Snipper.Web.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<Snippet, SnippetModel>();
            CreateMap<SnippetFile, SnippetFileModel>();
        }
    }
}
