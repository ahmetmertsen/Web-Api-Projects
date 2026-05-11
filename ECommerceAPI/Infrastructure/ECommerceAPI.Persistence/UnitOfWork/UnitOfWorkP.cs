using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.UnitOfWork;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.UnitOfWork
{
    public class UnitOfWorkP : IUnitOfWork
    {
        private readonly ECommerceDbContext _context;

        public ICustomerRepository CustomerRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICartItemRepository CartItemRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderItemRepository OrderItemRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IMenuRepository MenuRepository { get; }
        public IEndpointRepository EndpointRepository { get; }

        public UnitOfWorkP(ECommerceDbContext context, ICustomerRepository customerRepository, IAddressRepository addressRepository, ICartRepository cartRepository, ICartItemRepository cartItemRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository, IPaymentRepository paymentRepository, IMenuRepository menuRepository, IEndpointRepository endpointRepository)
        {
            _context = context;
            CustomerRepository = customerRepository;
            AddressRepository = addressRepository;
            CartRepository = cartRepository;
            CartItemRepository = cartItemRepository;
            OrderRepository = orderRepository;
            OrderItemRepository = orderItemRepository;
            ProductRepository = productRepository;
            PaymentRepository = paymentRepository;
            MenuRepository = menuRepository;
            EndpointRepository = endpointRepository;
        }

        public void Dispose() => _context.Dispose();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
    }
}
