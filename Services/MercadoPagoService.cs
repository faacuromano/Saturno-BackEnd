using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using System.Net.Http.Json;
using System.Text.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MercadoPago.Config;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;

namespace SATURNO_V2.Services;

public class MercadoPagoService
{
    //public void createPreference() {

    //    public url = "";

    //} 
    public List<string> GetRubro()
    {
        List<string> servicios = new List<string>()
        {
            "Psicologo",
            "Docente",
            "Canchas de futbol",
            "Peluquero",
            "Medico",
            "Estilista",
            "Kinesiologo"
        };
        return servicios;
    }
    public List<string> GetUbicaciones()
    {

        List<string> ubicaciones = new List<string>()
        {
            "Rosario",
            "Arroyo Seco",
            "Funes",
            "Villa Gobernador Galvez",
            "Roldan"
        };
        return ubicaciones;
    }

}