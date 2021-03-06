﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestLicenseService.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2015 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.LicenseManager.Services
{
    public interface ILicenseVisualizerService
    {
        /// <summary>
        /// Shows the single license dialog including all company info.
        /// </summary>
        /// <exception cref="System.Exception">Please use the Initialize method first</exception>
        void ShowLicense();
    }
}