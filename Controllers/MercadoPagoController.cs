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

namespace SATURNO_V2.Controllers;


[ApiController]
[Route("preapproval")]

public class MercadoPagoController : ControllerBase
{

    [HttpPost("mercadopagoPreference")]
    public async Task<IActionResult> RunAsync()
    {
        // Update port # in the following line.
        client.BaseAddress = new Uri("https://api.mercadopago.com/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new AuthenticationHeaderValue("Authorization", "Bearer APP_USR-2303800404167032-053121-7d6958f7ac334a6723eeaf0caf34f864-1387539311"));
        //new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("Authorization", "Bearer APP_USR-2303800404167032-053121-7d6958f7ac334a6723eeaf0caf34f864-1387539311");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Bearer APP_USR-2303800404167032-053121-7d6958f7ac334a6723eeaf0caf34f864-1387539311");
        //Authorization =
        //    new AuthenticationHeaderValue("Authorization", "Bearer APP_USR-2303800404167032-053121-7d6958f7ac334a6723eeaf0caf34f864-1387539311");


        try
        {
            // Create a new preference
            Preference preference = new Preference
            {
                payer_email = "test_user_603693144@testuser.com",
                back_url = "https://www.mercadopago.com.ar",
                status = "pending"
            };

            var url = await CreatePreferenceAsync(preference);
            Console.WriteLine($"Created at {url}");


        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;

        Console.ReadLine();
    }

    public HttpClient client = new HttpClient();

    [HttpPost]
    public async Task<Uri> CreatePreferenceAsync(Preference preference)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "https://api.mercadopago.com/preapproval", preference);
        response.EnsureSuccessStatusCode();

        // return URI of the created resource.
        return response.Headers.Location;
    }

    

    //[HttpPost]

    //public async Task<IActionResult> MpRequest()
    //{
    //    MercadoPagoConfig.AccessToken = "APP_USR-2303800404167032-053121-7d6958f7ac334a6723eeaf0caf34f864-1387539311";
    //    // Crea el objeto de request de la preference
    //    var request = new Preference
    //    {
    //        payer_email = "test_user_603693144@testuser.com",
    //        back_url = "https://www.mercadopago.com.ar",
    //        status = "pending"
    //    };
    
    //    // Crea la preferencia usando el client
    //    var client = new PreferenceClient();
    //    Preference preference = await client.CreateAsync(request);

    //    return request;

    //}



}



//1 

//public Task<IActionResult> Create();
//    {
//    string myJson = "{'back_url': 'https://www.mercadopago.com.ar', 'payer_email': 'test_user_603693144@testuser.com'}";

//var client = new HttpClient();

//var response = await client.PostAsync(
//    "http://yourUrl",
//     new StringContent(myJson, Encoding.UTF8, "application/json"));
//    }




// 2




// using (var client = new HttpClient())
// {
//     var response = await client.PostAsync(
//         "http://yourUrl",
//          new StringContent(myJson, Encoding.UTF8, "application/json"));
// }


//  string myJson = "{'preapproval_plan_id': 'null',
//       'reason': "Contratar Plan Profesional",
//       'payer_email': 'emailProf',
//       'auto_recurring':
//       {
//           'frequency': '1',
//         'frequency_type': "months",
//         'start_date': 'null',
//         'end_date': 'null',
//         'transaction_amount': '10',
//         'currency_id': "ARS"
//       },
//       'back_url': "/",
//       'status': "pending"}";
//       var post = await PostAsync("https://api.mercadopago.com/checkout/preferences",  new StringContent(myJson, Encoding.UTF8, "application/json"));