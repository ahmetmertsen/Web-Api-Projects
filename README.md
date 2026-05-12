# .NET Web API Projeleri

## 📌Projenin Ortak Özellikleri
- **Mimari & Patternlar**: Onion Architecture, Repository Pattern, UnitOfWork, CQRS + MediatR, FluentValidation
- **Teknolojiler**: .NET 6 Web API, Entity Framework Core, PostgreSQL, Swagger

---

## Proje 1: UserManagementAPI
Kullanıcı yönetimi için geliştirilmiş bir Web API projesidir.
- Kullanıcı ekleme, güncelleme, silme, listeleme işlemleri yapılabilir.

 **🔑EndPointler**
- GET /api/User/getAll → Tüm kullanıcıları getirir
- POST /api/User/create → Yeni kullanıcı ekler
- PUT /api/User/update → Kullanıcı günceller
- DELETE /api/User/delete/{id} → Kullanıcı siler

---

## Proje 2: HotelReservationAPI
Otel rezervasyon yönetimi için geliştirilmiş Web API projesidir.
- Müşteri yönetimi, oda yönetimi ve rezervasyon işlemleri yapılabilir.
- Ekstra Özellikler:
  - Exception Handler Middleware

**🔑Bazı EndPointler**
- PUT /api/Customer/update → Müşteri günceller
- GET /api/Room/getAll → Odaları listeler
- POST /api/Reservation/create → Mevcut Müşteri ile yeni rezervasyon yapar
- POST /api/Reservation/createWithCustomer → Yeni Müşteri bilgileri ile rezervasyon yapar
- GET /api/Reservation/getById/{id} → Rezervasyon detayını getirir

---

## Proje 3: ECommerceAPI
Basit bir e-ticaret uygulamasının Web API projesidir.
- Kullanıcı, Müşteri, Adres, Ürün, Sepet, Sipariş ve Ödeme yönetimi yapılabilir.
- Ekstra Özellikler:
  - Exception Handler Middleware
  - Identity + JWT ile Authentication/Authorization
  - Kullanıcı-rol ilişkilendirme ve kullanıcıya rol atama
  - Endpoint-rol yetkilendirme eşleştirmesi
  - Endpoint bazlı dinamik rol yetki kontrol filtresi

**🔑Bazı EndPointler**
- POST /api/User/create → Kullanıcı kaydı yapar
- POST /api/Auth/login → E-Posta ve Şifre ile kullanıcı girişi yönetimini yapar
- POST /api/User/assignRoleToUser → Kullanıcıya rol atar
- GET /api/User/getRolesToUser/{userId} → Kullanıcıya atanmış rolleri getirir
- POST /api/AuthorizationEndpoints → Endpoint'e rol atar
- POST /api/AuthorizationEndpoints/getRolesToEndpoint → Bir endpoint'e atanmış rolleri getirir
- POST /api/Order/create → Sipariş oluşturur, sepeti siler ve ürün stok güncellemesi yapar
- GET /api/Payment/getPaymentByOrderId/{orderId} → Sipariş id'sine göre ödeme bilgilerini getirir
- GET /api/Address/getAllByCustomerId/{customerId} → Müşterinin bütün adreslerini getirir

---
