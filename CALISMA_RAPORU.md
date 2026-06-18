# Çalışma Raporu — Akıllı Depo Yönetimi

### Özet

PDF’deki developer test kapsamında geliştirildi. Akış: ürün tanımla → depoya giriş (IN) → depodan çıkış (OUT).

Backend .NET 9 Web API, frontend React 18 + TypeScript + MUI, veritabanı MS SQL Server + EF Core migration.

### Teknolojiler

- Backend: .NET 9, EF Core 9, NArchitecture + MediatR (CQRS)
- DB: SQL Server (local/express)
- Frontend: React 18.3, TypeScript, MUI 9, Vite 8
- İlk iskelet: NArchitecture.Gen

### Mimari notlar

**Backend**  
PDF Controller → Manager → Repository öneriyor; proje NArchGen ile geldiği için MediatR handler + BusinessRules yapısı korundu. Manager’a denk düşüyor.

**Multi-tenant**  
Tüm entity’lerde `CompanyId` var. Boşsa 400, tenant uyuşmazsa 403 (`MultiTenantBusinessRules`).

**Soft delete**  
PDF `IsDeleted` istiyor; NArch tarafında `DeletedDate` + global filter kullanılıyor. Davranış aynı, alan adı şablondan farklı.

**HTTP**  
PUT/DELETE yok. `POST .../create`, `POST .../update`, `POST .../delete` (body’de Id + CompanyId).

**Domain**  
Company, Product, Warehouse, WarehouseSlot (Capacity, CurrentStock), StockMovement (IN/OUT). Stok hareketinde tip doğrulanır; OUT’ta yeterli stok, IN’de kapasite kontrolü; `CurrentStock` güncellenir.

**Frontend**  
Page / Platform component ayrımı: `pages/` (hook + state + API), `platform/components/` (UI), `services/` (HTTP, PascalCase body). CompanyId kullanıcı girer, Verileri Getir ile yüklenir.

**Sayfalama**  
Sunucu tarafı `PageIndex` / `PageSize`. Frontend varsayılan 10 kayıt (`client/src/config.ts`). Büyük projede bu değer config/env’den okunur; backend’de MaxPageSize sınırı konur — burada tek sabit yeterli.

**Seed migration**  
Reviewer için `SeedDemoData` migration eklendi (`Persistence/Seeding/`). `Update-Database` sonrası hazır veri:

- Tekno Çözüm — `0DAC2416-A292-4401-005C-08DECD47483A` — 12 ürün, 1 depo/raf, IN+OUT örneği
- Global Lojistik — `D4A57AE8-A65B-47E2-005D-08DECD47483A` — 3 ürün, 1 depo/raf, IN örneği

Boş DB’de çalışır. Aynı GUID’ler varsa çakışır; temiz deneme için DB drop + migration tekrar.

### PDF’den bilinçli sapmalar

- Manager → MediatR handler (NArch şablonu)
- IsDeleted → DeletedDate (NArch entity tabanı)
- Açık EntityState.Modified → repository UpdateAsync / change tracking


** Connection string ve SQL ortamı yerelde ayarlanmalı.**

### Çalıştırma

1. `appsettings.Development.json` → `ConnectionStrings:BaseDb` kendi SQL instance’ınıza göre
2. Migration:

```powershell
Update-Database -Project Persistence -StartupProject WebAPI -Context BaseDbContext
```

3. Backend:

```bash
cd src/akilliDepo/WebAPI
dotnet run
```

Swagger: http://localhost:5278/swagger

4. Frontend:

```bash
cd client
npm install
npm run dev
```

http://localhost:5173 — CompanyId yapıştır (yukarıdaki GUID’lerden biri) → Verileri Getir.

CLI alternatif: `dotnet ef database update --project src/akilliDepo/Persistence --startup-project src/akilliDepo/WebAPI`

### Klasör yapısı

```
AkilliDepo/
├── CALISMA_RAPORU.md
├── client/src/pages, platform, services
└── src/akilliDepo/
    ├── WebAPI
    ├── Application
    ├── Persistence   (Migrations, Seeding)
    └── Domain
```
**Ozan YİĞİT**