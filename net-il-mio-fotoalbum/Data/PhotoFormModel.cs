﻿using Microsoft.AspNetCore.Mvc.Rendering;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Data
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public List<string>? SelectedCategories { get; set; }

        public PhotoFormModel() { }

        public PhotoFormModel(Photo photo)
        {
            Photo = photo;
            SelectedCategories = new List<string>();
            if (Photo.Categories != null)
            {
                foreach (var i in Photo.Categories)
                    SelectedCategories.Add(i.Id.ToString());
            }
        }
        public void CreateCategories()
        {
            this.Categories = new List<SelectListItem>();
            if (this.SelectedCategories == null)
            {
                this.SelectedCategories = new List<string>();
            }
            var categoriesDB = PhotoManager.GetAllCategories();
            foreach (var cat in categoriesDB)
            {
                bool selected = this.SelectedCategories.Contains(cat.Id.ToString());
                this.Categories.Add(new SelectListItem()
                {
                    Text = cat.Name,
                    Value = cat.Id.ToString(),
                    Selected = selected
                });
            }
        }
    }
}
