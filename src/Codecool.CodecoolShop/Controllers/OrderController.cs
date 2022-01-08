using System;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Codecool.CodecoolShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ClientService _clientService;

        public OrderController(IOrderItemDao orderItemDao, IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao supplierDao, IShoppingCartDao shoppingCartDao,
            IShoppingCartItemDao shoppingCartItemDao, IClientDao clientDao, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = new OrderService(orderItemDao, productDao, productCategoryDao, supplierDao, shoppingCartDao, shoppingCartItemDao, httpContextAccessor);
            _clientService = new ClientService(clientDao);
        }

        [HttpGet]
        public IActionResult CompletingOrderData(string userId)
        {
            OrderViewModel orderViewModel = new OrderViewModel
            {
                Order = new OrderItem
                {
                    UserId = userId,
                    ProductsIds = JsonConvert.SerializeObject(_orderService.GetProductsIdsByUserId(userId)),
                    Currency = "USD",
                    TotalPrice = _orderService.GetTotalPrice(userId),
                    IsPaid = false
                },
                ValidatorClientModel = new Client
                {
                    UserId = userId,
                }
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult CompletingOrderData(OrderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Order.Client = viewModel.ValidatorClientModel;
                _clientService.AddClient(viewModel.Order.Client);
                return RedirectToAction("CompletingOrderPayment", viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CompletingOrderPayment(OrderViewModel viewModel) => View(viewModel);

        [HttpPost]
        [ActionName("CompletingOrderPayment")]
        public IActionResult CompletingOrderPaymentPost(OrderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Order.OrderDate = DateTime.Now;
                viewModel.Order.IsPaid = true;
                _orderService.AddFinalizedOrder(viewModel.Order);
                _orderService.ClearCartItems(viewModel.Order.UserId);
                return RedirectToAction("OrderCompleted", viewModel);
            }

            return View(viewModel);
        }

        public IActionResult OrderCompleted(OrderViewModel viewModel) => View(viewModel);
    }
}