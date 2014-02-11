﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2014 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.LicenseManager.Client.Example.ViewModels
{
    using Catel;
    using Catel.MVVM;
    using Services;

    /// <summary>
    /// MainWindow view model.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {

        #region Fields
        private readonly ILicenseService _licenseService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel(ILicenseService licenseService)
            : base()
        {
            Argument.IsNotNull(() => licenseService);

            _licenseService = licenseService;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the title of the view model.
        /// </summary>
        /// <value>The title.</value>
        public override string Title
        {
            get { return "License manager example"; }
        }
        #endregion

        protected override void Initialize()
        {
            if (_licenseService.LicenseExists())
            {
                var licenseString = _licenseService.LoadLicense();
                var licenseValidation = _licenseService.ValidateLicense(licenseString);

                if (licenseValidation.HasErrors)
                {
                    _licenseService.ShowSingleLicenseDialog("CatelSoftware", "http://www.catelproject.com");
                }
            }
            else
            {
                _licenseService.ShowSingleLicenseDialog("CatelSoftware", "http://www.catelproject.com");
            }
        }
    }
}