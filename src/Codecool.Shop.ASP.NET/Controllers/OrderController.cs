using System;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.ASP.NET.ViewModels;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Codecool.Shop.ASP.NET.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IClientService _clientService;

    public OrderController(IOrderService orderService,
                             IClientService clientService)
    {
        _orderService = orderService;
        _clientService = clientService;
    }

    [HttpGet]
    public IActionResult CompletingOrderData()
    {
        string userId = Request.Cookies["userId"];
        OrderViewModel orderViewModel = new()
        {
            Order = new Order
            {
                UserId = userId,
                ProductsIds = JsonConvert.SerializeObject(_orderService.GetProductsIds(userId)),
                Currency = "USD",
                TotalPrice = _orderService.GetTotalPrice(userId),
                IsPaid = false
            },
            ValidatorClientModel = new Client
            {
                UserId = userId,
            },
            CartItems = _orderService.GetCartItems(userId),
            TotalPrice = _orderService.GetTotalPrice(userId),
        };
        _orderService.AddOrder(orderViewModel.Order);

        return View(orderViewModel);
    }

    [HttpPost]
    // [Route("/CompletingOrderData/{userId}")]
    public IActionResult CompletingOrderData(OrderViewModel viewModel)
    {
        string userId = Request.Cookies["userId"];

        if (ModelState.IsValid)
        {
            Order order = _orderService.GetOrder(userId);
            order.Client = viewModel.ValidatorClientModel;
            _clientService.AddClient(order.Client);
            _orderService.Modify(order);

            viewModel.CartItems = _orderService.GetCartItems(userId);
            viewModel.TotalPrice = _orderService.GetTotalPrice(userId);

            return RedirectToAction("CompletingOrderPayment", viewModel);
        }

        // OrderViewModel orderViewModel = new OrderViewModel
        // {
        //     Order = _orderService.GetOrder(userId),
        //     ValidatorClientModel = new Client
        //     {
        //         UserId = userId,
        //     },
        //     CartItems = _orderService.GetCartItemsByUserId(userId),
        //     TotalPrice = _orderService.GetTotalPrice(userId),
        // };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult CompletingOrderPayment(OrderViewModel viewModel)
    {
        string userId = Request.Cookies["userId"];
        OrderViewModel orderViewModel = new()
        {
            Order = _orderService.GetOrder(userId),
            ValidatorClientModel = _clientService.GetClient(userId),
            CartItems = _orderService.GetCartItems(userId),
            TotalPrice = _orderService.GetTotalPrice(userId),
        };

        return View(orderViewModel);
    }

    [HttpPost]
    [ActionName("CompletingOrderPayment")]
    public IActionResult CompletingOrderPaymentPost(OrderViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            string userId = Request.Cookies["userId"];
            Order order = _orderService.GetOrder(userId);
            order.DateCreated = DateTime.Now;
            order.IsPaid = true;
            _orderService.Modify(order);
            _orderService.RemoveCart(order.UserId);
            _orderService.RemoveCartItems(order.UserId);

            return RedirectToAction("OrderCompleted", _orderService.GetOrder(userId));
        }

        return View(viewModel);
    }

    public IActionResult OrderCompleted(Order orderCompleted) => View(orderCompleted);
}