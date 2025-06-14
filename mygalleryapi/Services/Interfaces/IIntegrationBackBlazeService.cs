﻿using APIGallery.DTO;
using APIGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGallery.Services.Interfaces
{
    public interface IIntegrationBackBlazeService
    {
        Task<ResponseDTO<BackBlazeAuthResponse>> GetAuthAsync();
        Task<ResponseDTO<BackBlazeGetUrlResponse>> GetUploadUrlAsync();
        Task<ResponseDTO<BackBlazeUploadResponse>> UploadFileAsync(string nomeArquivo, Stream conteudo, string contentType);
    }
}
