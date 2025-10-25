# CmdCs - Komut Satırı Yorumlayıcısı (v.1.12.x)

[_English v.1.12_](document_cmdcs_v.1.12.x.ing.md)

`CmdCs`, standart Windows komut satırının (CMD) yeteneklerini C# benzeri sözdizimi ile zenginleştiren ve bu komutları çalıştırılabilir Batch (`.bat`) dosyalarına çevirebilen gelişmiş bir komut satırı aracıdır. Bu proje, hem interaktif bir kabuk olarak çalışabilir hem de C# benzeri döngü ve değişken mantığını içeren kod bloklarını alıp doğrudan `.bat` script'lerine dönüştürebilir.

## ✨ Temel Özellikler

- **C# Benzeri Sözdizimi:** `for`, `while`, `do-while` döngüleri, `int`, `string` değişken tanımlamaları ve matematiksel işlemler için C# diline benzer bir yapı sunar.
- **Batch Dosyası Üretimi:** `public class <DosyaAdı> : bat { ... }` yapısını kullanarak yazdığınız C# benzeri kodları otomatik olarak geçerli bir `.bat` dosyasına çevirir.
- **Değişken Yönetimi:** `$degisken` formatıyla değişken kullanma, `set`, `int`, `string` ile tanımlama ve matematiksel işlemler yapma imkanı.
- **Dizi Desteği:** `int[]` ve `string[]` formatında diziler tanımlama ve `foreach` döngüsü ile bu diziler üzerinde gezinme.
- **Koşullu Komutlar:** CMD komutlarının çıktısına göre farklı komutlar çalıştıran `ternary` operatör desteği (`koşul ? doğruysa : yanlışsa`).
- **Yapılandırma Dosyası:** `Setting.cs` dosyası üzerinden başlangıç ayarlarını (renkler, notlar vb.) yönetme.
- **Genişletilebilir Komut Seti:** `help`, `hdd` gibi dahili yardımcı komutlar.

## Yapılandırma (Setting.txt)

Uygulamanın davranışını `Setting.txt` dosyasını düzenleyerek özelleştirebilirsiniz.

| Ayar          | Örnek Değer         | Açıklama                                                                                                                                      |
| ------------- | ------------------- | --------------------------------------------------------------------------------------------------------------------------------------------- |
| `linecolor`   | `Yellow,White`      | Komut isteminin (`>>`) iki alternatif rengini ayarlar.                                                                                        |
| `defaultpage` | `Display1`          | Başlangıç mesajını değiştirir. `Display1` İngilizce karşılama mesajını, diğer herhangi bir değer (örn: `Display2`) ise Türkçe olanı gösterir. |
| `note`        | `true` veya `false` | `true` olarak ayarlandığında başlangıçta ek notlar gösterir. `false` olduğunda ise gizler.                                                    |

## 🚀 Kullanım

## Genel

| Komut            | Açıklama                                         |
| ---------------- | ------------------------------------------------ |
| `help2`, `help3` | Yardım menülerini gösterir.                      |
| `hdd`            | C sürücüsündeki boş alanı GB cinsinden gösterir. |
| `cls`            | Ekranı temizler.                                 |
| # yorum          | Yorum satırı.                                    |
| // yorum         | Yorum satırı.                                    |

## Değişken

| ------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| `set a=10` | Bir değişken tanımlar. Matematiksel ifadeler de kullanılabilir. |
| `int a=10` | Tamsayı (integer) tipinde bir değişken tanımlar. |
| `string a="hello"` | Metin (string) tipinde bir değişken tanımlar. |
| `echo $a` | `a` değişkeninin değerini ekrana basar. |

```bash
# Sayısal bir değişken tanımla
int i = 10

# Değişkenin değerini ekrana yazdır
echo $i

# Matematiksel bir işlemle yeni bir değişken tanımla
set j = $i * 2

# j'nin değerini gör
echo $j
```

## Dizi

| ------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| `int[] nums={1,2,3}` | Tamsayı dizisi tanımlar. |
| `string[] arr={"a","b"}` | Metin dizisi tanımlar. |

```bash
# Sayısal bir değişken tanımla
int[] nums={1,2,3}

# Metin bir değişken tanımla
string[] arr={"a","b"}

# Değişkenin değerini ekrana yazdır
echo $nums

# Değişkenin değerini ekrana yazdır
echo $arr

```

## Döngüler

| ------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| `for (int i=0; i<5; i++){...}` | Belirtilen koşul sağlandığı sürece kod bloğunu çalıştıran `for` döngüsü. |
| `while ($i<5){...}` | Koşul doğru olduğu sürece çalışan `while` döngüsü. |
| `do {...} while ($i<5){...}` | Önce bloğu çalıştırıp sonra koşulu kontrol eden `do-while` döngüsü. |
| `foreach ($item in $arr[]){...}` | Bir dizi içerisindeki her bir eleman için kod bloğunu çalıştırır. |

**For Döngüsü:**

```csharp
# 0'dan 4'e kadar sayıları ekrana yazdırır
for (int i=0; i<5; i++) { echo $i }
```

**While Döngüsü:**

```csharp
# i değişkenini 5'ten küçük olduğu sürece ekrana yazdırır ve her adımda bir artırır
int i=0
while ($i < 5) { echo $i && i++ }
```

**Do While Döngüsü:**

```csharp
# do bloğu bir kez çalışır, ardından while koşulu doğru olduğu sürece döngü devam eder.
 do { set i=0 } while ( $i < 5 ) { echo $i && i++ }
```

**Foreach Döngüsü:**

```csharp
# a dizisini foreach ile gezerek her bir elemanı ekrana yazdırır
 int[] a={1,2,3}
 foreach ($b in $a[]) { echo $b }

```

## Ternary

| ------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| `dir \| find "test" ? echo Var : echo Yok` | `dir` komutunun çıktısında "test" varsa "Var", yoksa "Yok" yazar. |

```bash
# dir komutunun çıktısını kontrol eder
 dir | find "z" ? echo evet : echo hayır
```

## Dosya yazıcı

| ------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| `public class Ad : uzanti { ... }` | Belirtilen ad ve uzantıda bir dosya oluşturur ve içerik yazma modunu başlatır. `}` ile sonlanır. |

`CmdCs`'in geliştirlmesi gereken özelliklerinden biri, yazdığınız C# benzeri kodları çalıştırılabilir bir `.bat` dosyasına çevirmeyi hedeflicek. Bu, `public class` sözdizimi ile yapılır.

**Örnek:**
Aşağıdaki komutları `CmdCs` konsoluna girdiğinizde, `DonguTest.bat` adında bir dosya oluşturulacaktır.

1.  Dosya yazma modunu başlatmak için aşağıdaki komutu girin:

    ```csharp
    public class DonguTest : bat {
    ```

2.  Ardından, `.bat` dosyasına çevrilmesini istediğiniz kodları yazın:

    ```csharp
    rem Bu bir for dongusu testidir
    rem herhangi bat komutları
    echo Dongu bitti!
    ```

3.  Yazma işlemini bitirmek için `}` karakterini girin:
    ```csharp
    }
    ```

**Oluşturulan `DonguTest.bat` Dosyasının İçeriği:**

```batch
@echo off
rem Bu bir for dongusu testidir
rem herhangi bat komutları
echo Dongu bitti!
```
