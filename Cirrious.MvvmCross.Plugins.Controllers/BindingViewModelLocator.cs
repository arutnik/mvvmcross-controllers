﻿using Cirrious.MvvmCross.ViewModels;
using System;

namespace Cirrious.MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// A ViewModelLocator that uses a core locator implementation and then a binder
    /// to bind a Controller to the ViewModel
    /// </summary>
    public class BindingViewModelLocator : IMvxViewModelLocator
    {
        private readonly IMvxViewModelLocator _coreViewModelLocator;
        private readonly IViewModelBinder _viewModelBinder;

        public BindingViewModelLocator(IMvxViewModelLocator coreViewModelLocator, IViewModelBinder viewModelBinder)
        {
            if (coreViewModelLocator == null) throw new ArgumentNullException("coreViewModelLocator");
            if (viewModelBinder == null) throw new ArgumentNullException("viewModelBinder");

            _coreViewModelLocator = coreViewModelLocator;
            _viewModelBinder = viewModelBinder;
        }

		public IMvxViewModel Load (Type viewModelType, IMvxBundle parameterValues, IMvxBundle savedState)
		{
			var viewModel = _coreViewModelLocator.Load(viewModelType, parameterValues, savedState);

			if (viewModel != null)
			{
				_viewModelBinder.Bind(viewModel, parameterValues, savedState);
			}

			return viewModel;
		}
    }
}