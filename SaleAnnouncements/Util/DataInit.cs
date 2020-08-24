using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.DAL.Repositories;

namespace SaleAnnouncements.Util
{
	public class DataInit
	{
		public static async Task InitData(ICategoryService categoryService,IOfferStatusService offerStatusService)
		{
			var categories = await categoryService.GetAll();
			var statuses = await offerStatusService.GetAll();

			if (categories.Count == 0)
			{
				var newCategories = new List<CategoryDto>
				{
					new CategoryDto
					{
						Name = "Автомобили"
					},
					new CategoryDto
					{
						Name = "Бытовая техника"
					},
					new CategoryDto
					{
						Name = "Офисная техника"
					},
					new CategoryDto
					{
						Name = "Стройматериалы"
					}
				};

				await categoryService.AddCollection(newCategories);
			}

			if (statuses.Count == 0)
			{
				var newStatuses = new List<OfferStatusDto>
				{
					new OfferStatusDto
					{
						Amount = 2, Name = "Поднять на 2 позиции", Price = 100m
					},
					new OfferStatusDto
					{
						Amount = 5, Name = "Поднять на 5 позиций", Price = 200m
					},
					new OfferStatusDto
					{
						Amount = 10, Name = "Поднять на 10 позиций", Price = 400m
					}
				};

				await offerStatusService.AddCollection(newStatuses);
			}
		}
	}
}
