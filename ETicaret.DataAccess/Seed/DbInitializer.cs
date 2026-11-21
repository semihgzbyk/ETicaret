using ETicaret.DataAccess.Context;
using ETicaret.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.DataAccess.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<AppDbContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var config = services.GetRequiredService<IConfiguration>();

        // Roller
        foreach (var role in new[] { "Admin", "Customer" })
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Admin kullanıcı
        const string adminEmail = "admin@eticaret.com";
        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var adminPassword = config["Seed:AdminPassword"] ?? "Admin123!";
            var admin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Site Yöneticisi",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }

        // Kategoriler
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Elektronik", Description = "Telefon, bilgisayar ve aksesuarlar", ImageUrl = "/images/products/placeholder.jpg" },
                new Category { Name = "Giyim", Description = "Kadın, erkek ve çocuk giyim", ImageUrl = "/images/products/placeholder.jpg" },
                new Category { Name = "Ev & Yaşam", Description = "Dekorasyon ve mutfak ürünleri", ImageUrl = "/images/products/placeholder.jpg" },
                new Category { Name = "Kitap", Description = "Roman, kişisel gelişim ve eğitim", ImageUrl = "/images/products/placeholder.jpg" },
                new Category { Name = "Spor", Description = "Spor giyim ve ekipmanları", ImageUrl = "/images/products/placeholder.jpg" },
                new Category { Name = "Kozmetik", Description = "Cilt bakımı ve makyaj", ImageUrl = "/images/products/placeholder.jpg" }
            );
            await context.SaveChangesAsync();
        }

        // Markalar
        if (!context.Brands.Any())
        {
            context.Brands.AddRange(
                new Brand { Name = "TechNova" },
                new Brand { Name = "UrbanWear" },
                new Brand { Name = "HomeNest" },
                new Brand { Name = "PageTurn" },
                new Brand { Name = "ActiveFit" },
                new Brand { Name = "GlowCare" }
            );
            await context.SaveChangesAsync();
        }

        // Ürünler
        if (!context.Products.Any())
        {
            int Cat(string name) => context.Categories.First(c => c.Name == name).Id;
            int Brd(string name) => context.Brands.First(b => b.Name == name).Id;
            const string img = "/images/products/placeholder.jpg";

            context.Products.AddRange(
                // Elektronik
                new Product { Name = "Akıllı Telefon X1", Description = "6.5 inç ekran, 128 GB hafıza", Price = 14999m, DiscountPrice = 13499m, Stock = 25, ImageUrl = img, IsFeatured = true, CategoryId = Cat("Elektronik"), BrandId = Brd("TechNova") },
                new Product { Name = "Kablosuz Kulaklık", Description = "Aktif gürültü engelleme", Price = 2499m, DiscountPrice = 1999m, Stock = 60, ImageUrl = img, IsFeatured = true, CategoryId = Cat("Elektronik"), BrandId = Brd("TechNova") },
                new Product { Name = "Dizüstü Bilgisayar Pro", Description = "16 GB RAM, 512 GB SSD", Price = 32999m, Stock = 12, ImageUrl = img, IsFeatured = true, CategoryId = Cat("Elektronik"), BrandId = Brd("TechNova") },
                new Product { Name = "Akıllı Saat", Description = "Nabız ve uyku takibi", Price = 3499m, DiscountPrice = 2999m, Stock = 40, ImageUrl = img, CategoryId = Cat("Elektronik"), BrandId = Brd("TechNova") },
                // Giyim
                new Product { Name = "Erkek Basic Tişört", Description = "%100 pamuk", Price = 399m, Stock = 150, ImageUrl = img, CategoryId = Cat("Giyim"), BrandId = Brd("UrbanWear") },
                new Product { Name = "Kadın Kot Pantolon", Description = "Yüksek bel, slim fit", Price = 899m, DiscountPrice = 749m, Stock = 80, ImageUrl = img, IsFeatured = true, CategoryId = Cat("Giyim"), BrandId = Brd("UrbanWear") },
                new Product { Name = "Kapüşonlu Sweatshirt", Description = "İçi polarlı, unisex", Price = 699m, Stock = 90, ImageUrl = img, CategoryId = Cat("Giyim"), BrandId = Brd("UrbanWear") },
                new Product { Name = "Deri Ceket", Description = "Hakiki deri", Price = 4299m, DiscountPrice = 3799m, Stock = 15, ImageUrl = img, CategoryId = Cat("Giyim"), BrandId = Brd("UrbanWear") },
                // Ev & Yaşam
                new Product { Name = "Seramik Kupa Seti", Description = "6'lı set", Price = 349m, Stock = 70, ImageUrl = img, CategoryId = Cat("Ev & Yaşam"), BrandId = Brd("HomeNest") },
                new Product { Name = "Dekoratif Yastık", Description = "45x45 cm kadife", Price = 199m, DiscountPrice = 149m, Stock = 120, ImageUrl = img, CategoryId = Cat("Ev & Yaşam"), BrandId = Brd("HomeNest") },
                new Product { Name = "Nevresim Takımı", Description = "Çift kişilik, pamuklu", Price = 1299m, Stock = 35, ImageUrl = img, IsFeatured = true, CategoryId = Cat("Ev & Yaşam"), BrandId = Brd("HomeNest") },
                new Product { Name = "LED Masa Lambası", Description = "Dokunmatik, 3 ışık modu", Price = 449m, Stock = 55, ImageUrl = img, CategoryId = Cat("Ev & Yaşam"), BrandId = Brd("HomeNest") },
                // Kitap
                new Product { Name = "Temiz Kod", Description = "Yazılım geliştirme klasiği", Price = 329m, Stock = 45, ImageUrl = img, IsFeatured = true, CategoryId = Cat("Kitap"), BrandId = Brd("PageTurn") },
                new Product { Name = "Algoritmalara Giriş", Description = "Üniversite ders kitabı", Price = 549m, DiscountPrice = 479m, Stock = 30, ImageUrl = img, CategoryId = Cat("Kitap"), BrandId = Brd("PageTurn") },
                new Product { Name = "Kişisel Gelişim Rehberi", Description = "Alışkanlık ve verimlilik", Price = 189m, Stock = 100, ImageUrl = img, CategoryId = Cat("Kitap"), BrandId = Brd("PageTurn") },
                new Product { Name = "Tarihten Hikayeler", Description = "Çizimli çocuk kitabı", Price = 129m, Stock = 85, ImageUrl = img, CategoryId = Cat("Kitap"), BrandId = Brd("PageTurn") },
                // Spor
                new Product { Name = "Koşu Ayakkabısı", Description = "Hafif, nefes alan taban", Price = 1899m, DiscountPrice = 1599m, Stock = 50, ImageUrl = img, IsFeatured = true, CategoryId = Cat("Spor"), BrandId = Brd("ActiveFit") },
                new Product { Name = "Yoga Matı", Description = "6 mm, kaymaz yüzey", Price = 299m, Stock = 95, ImageUrl = img, CategoryId = Cat("Spor"), BrandId = Brd("ActiveFit") },
                new Product { Name = "Ayarlanabilir Dambıl Seti", Description = "2x10 kg", Price = 1199m, Stock = 25, ImageUrl = img, CategoryId = Cat("Spor"), BrandId = Brd("ActiveFit") },
                new Product { Name = "Spor Çantası", Description = "35 litre, su geçirmez", Price = 549m, DiscountPrice = 449m, Stock = 60, ImageUrl = img, CategoryId = Cat("Spor"), BrandId = Brd("ActiveFit") },
                // Kozmetik
                new Product { Name = "Nemlendirici Krem", Description = "Tüm cilt tipleri, 50 ml", Price = 259m, Stock = 110, ImageUrl = img, IsFeatured = true, CategoryId = Cat("Kozmetik"), BrandId = Brd("GlowCare") },
                new Product { Name = "Güneş Koruyucu SPF 50", Description = "Yüz için, 50 ml", Price = 319m, DiscountPrice = 279m, Stock = 75, ImageUrl = img, CategoryId = Cat("Kozmetik"), BrandId = Brd("GlowCare") },
                new Product { Name = "Mat Ruj", Description = "Uzun süre kalıcı", Price = 189m, Stock = 130, ImageUrl = img, CategoryId = Cat("Kozmetik"), BrandId = Brd("GlowCare") },
                new Product { Name = "Saç Bakım Serumu", Description = "Onarıcı, 100 ml", Price = 229m, Stock = 90, ImageUrl = img, CategoryId = Cat("Kozmetik"), BrandId = Brd("GlowCare") }
            );
            await context.SaveChangesAsync();
        }
    }
}