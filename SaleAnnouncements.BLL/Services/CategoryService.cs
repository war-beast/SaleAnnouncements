using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;

namespace SaleAnnouncements.BLL.Services
{
	public class CategoryService : ServiceBase, ICategoryService
	{
		#region private members

		private readonly IMapper _mapper;

		#endregion

		#region constructor

		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		#endregion

		public async Task<IReadOnlyCollection<CategoryDto>> GetAll()
		{
			var categories = _unitOfWork.Categories.GetAll();
			return await Task.Run(() => _mapper.Map<List<CategoryDto>>(categories));
		}

		public async Task<CategoryDto> Get(Guid id) =>
			_mapper.Map<CategoryDto>(await _unitOfWork.Categories.Get(id));

		public async Task AddCollection(IEnumerable<CategoryDto> items)
		{
			var coreCategories = _mapper.Map<IEnumerable<Category>>(items);

			foreach (var category in coreCategories)
			{
				_unitOfWork.Categories.Create(category);
			}

			await _unitOfWork.SaveAsync();
		}
	}
}
