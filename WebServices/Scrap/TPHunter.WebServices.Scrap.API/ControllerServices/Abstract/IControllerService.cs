﻿using System;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.WebServices.Scrap.API.ControllerServices.Abstract
{
    public interface IControllerService<in TModel> where TModel:IModel
    {
        public Task InsertAsync(TModel model);
        public Task UpdateAsync(TModel model);
        public Task RemoveAsync(string applicationNumber);
        public Task RemoveAsync(Guid ıd);
    }
}
