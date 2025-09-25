# Order Management Project / پروژه مدیریت سفارشات

[![Build Status](https://img.shields.io/badge/build-pending-lightgrey)](https://example.com)  [![.NET](https://img.shields.io/badge/.NET-7-blue)](https://dotnet.microsoft.com/)  [![License: MIT](https://img.shields.io/badge/license-MIT-green)](./LICENSE)

---

## 🇮🇷 معرفی (فارسی)

سلام 👋 من **پویا لاریان** هستم.
این پروژه حدود **۶ ساعت** زمان برده و آماده ارسال است.

### معماری و پیاده‌سازی
- پیاده‌سازی با **معماری DDD** و الگوی **CQRS**.
- احراز هویت با **JWT** برای امنیت برنامه.
- پایگاه داده **SQLite** در مسیر اصلی (root) پروژه قرار دارد.

> اگر می‌خواهید فایل پایگاه داده را حذف کنید و پایگاه داده را مجدداً بسازید از دستور زیر استفاده کنید:

```bash
dotnet ef database update -p Ordermanagement.Infrastructure -s Ordermanagement.Api -c WriteDbContext
```

### مقداردهی اولیه (Seeding)
- در اولین اجرای پروژه جدول `Users` به صورت خودکار با مقادیر پیش‌فرض پر می‌شود.
- مقادیر و منطق مقداردهی اولیه را می‌توانید در فایل‌های مربوط به **Seed** پیدا کنید — دنبال متد `SeedAsync` در پروژه‌ی `Ordermanagement.Infrastructure` بگردید.

### استانداردها و کیفیت کد
- تلاش شده اصول **Clean Code** و **SOLID** رعایت شود.
- برخی متدها به‌صورت کمّی و صرفاً برای **تست** و نمایش عملکردها اضافه شده‌اند؛ این متدها را در صورت دلخواه می‌توانید نادیده بگیرید یا حذف کنید.
- در بخش **Dependency Injection** پیاده‌سازی ساده‌ شده است چون هدف پروژه تستی بوده و زمان توسعه محدود بوده است.

### تست
- یک مجموعه تست در قالب **Postman** در پوشه‌ی `postman/` قرار گرفته.
- در حالت ایده‌آل و در صورت داشتن زمان بیشتر، پیاده‌سازی **Unit Tests** و **Integration Tests** به جای تکیه صرف به Postman پیشنهاد می‌شد.

### اجرا (Quick Start)
1. کد را کلون کنید:

```bash
git clone <your-repo-url>
cd <repo-folder>
```

2. بازیابی بسته‌ها و ساخت پروژه:

```bash
dotnet restore
dotnet build
```

3. اجرای مهاجرت و ساخت پایگاه داده (در صورت حذف فایل DB):

```bash
dotnet ef database update -p Ordermanagement.Infrastructure -s Ordermanagement.Api -c WriteDbContext
```

4. اجرای پروژه (نمونه):

```bash
dotnet run --project Ordermanagement.Api
```

> توجه: در صورت نیاز به Environment خاص (مثلاً `ASPNETCORE_ENVIRONMENT=Development`) آن را مطابق با کانفیگ خود تنظیم کنید.

### ساختار فایل‌ها (نمونه)
```
/Ordermanagement
  /Ordermanagement.Api
  /Ordermanagement.Infrastructure
  /Ordermanagement.Application
  /Ordermanagement.Domain
  /postman
  README.md
  *.db (SQLite database file)
```

### نکات شناخته‌شده
- تست‌ها (Unit / Integration) تکمیل نشده‌اند.
- پیاده‌سازی DI برای سادگی مختصر شده؛ برای تولیدی‌سازی پیشنهاد می‌شود ساختار DI بازنگری شود.

### تماس
- 📞 09376363535
- 📧 pooya.laryan@gmail.com

---

## 🇬🇧 Introduction (English)

Hi 👋 I'm **Pooya Laryan**.
This project took around **6 hours** to complete and is ready to deliver.

### Architecture & Implementation
- Implemented using **DDD** architecture with the **CQRS** pattern.
- **JWT** is used for authentication and security.
- The database is **SQLite** and located in the project root.

> If you prefer to delete the database file and recreate the DB, run:

```bash
dotnet ef database update -p Ordermanagement.Infrastructure -s Ordermanagement.Api -c WriteDbContext
```

### Data Seeding
- On first run, the `Users` table is seeded with default values.
- Check the **Seed** implementation (look for the `SeedAsync` method in `Ordermanagement.Infrastructure`).

### Code Quality
- Efforts made to follow **Clean Code** and **SOLID** principles.
- Some extra helper/test methods were added for demonstration/testing and can be ignored or removed.
- Dependency Injection is simplified for this test project — consider refactoring for production use.

### Testing
- A **Postman** collection is included in the `postman/` folder.
- With more time, prefer **Unit Tests** and **Integration Tests** instead of relying only on Postman.

### Quick Start
1. Clone the repo:

```bash
git clone <your-repo-url>
cd <repo-folder>
```

2. Restore and build:

```bash
dotnet restore
dotnet build
```

3. Apply migrations / recreate DB (if needed):

```bash
dotnet ef database update -p Ordermanagement.Infrastructure -s Ordermanagement.Api -c WriteDbContext
```

4. Run the API:

```bash
dotnet run --project Ordermanagement.Api
```

### Project Layout (example)
```
/Ordermanagement
  /Ordermanagement.Api
  /Ordermanagement.Infrastructure
  /Ordermanagement.Application
  /Ordermanagement.Domain
  /postman
  README.md
  *.db (SQLite database file)
```

### Known Notes
- Unit / Integration tests are not included yet.
- DI setup is intentionally simplified for the scope of this project.

### Contact
- 📞 09376363535
- 📧 pooya.laryan@gmail.com

---

## Contribution
If you'd like to contribute, please open an issue or a pull request. For larger changes, open an issue to discuss the plan first.

## License
This repository does not include a license file by default. If you want to publish under an open-source license, add a `LICENSE` file (e.g. MIT).

---

> If you want, I can:
> - add real badges (build, coverage, dotnet version) with links,
> - generate a `LICENSE` file (MIT/Apache/etc.),
> - or produce a minimal GitHub Actions workflow to run build/tests.


