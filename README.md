# Growing_Gems

Rastgele renklerden oluşan gemlerin toplanıp satışının yapılması amaçlanmıştır. Bu gemler, gridler üstünde oluşur. Joystick ile hareket eden karakter, gemleri alırsa sırtında stackler. Karakter, satış alanına girince gemleri yok ederek satmış olur. Oyuncu ekranın sağında bulunan bir buton ile yaptığı satışların sayısını bir Pop-up aracalığıyla görebilir. Kazanılan gold ve hangi gemlerden ne kadar satıldığının bilgisini oyundan çıkılsa bile kaydeden bir save sistemi mevcuttur.

## Önemli Bilgiler:

- Oyunda editor tarafında, istenen satır ve sütün değerleri girilip sonrasında butona basarak grid oluşturabilen veya silebilen bir sistem mevcuttur. GridMaker isimli script herhangi bir objeye atanırsa, o obje grid üretebilir hale gelmektedir.
- GemManager adlı Gameobject, aynı isimli bir scripte sahiptir. Bu objenin inspectorundan istenildiği kadar gem tipi eklenebilir veya çıkarılabilir.
- Oyundaki save sistemi, yeni gem tipi eklenince ya da çıkarılınca düzgün şekilde çalışmaya devam etmektedir. Gem eklenirse, ilgili gemin model ve count bilgileri slot açılıp oraya kaydedilmektedir. Gem oyundan çıkarılınca bile datalar o slotta duracağı için, sonradan yine oyuna eklenmeye karar verilirse ilgili datalar da tekrar pop-up penceresine aktarılır.
- Bir gem çeşidi İsim, Başlangıç Satış Fiyatı, Gem ikonu ve Spawnlanacak Gem Modeli bilgilerine sahiptir.
- Ekranın sağındaki buton Pop-up’ı açar. Burada icon, gem ismi, bu gemden toplanan sayı bilgileri vardır. Yeni gem çeşitleri eklenir ya da silinirse bu liste de dinamik olarak güncellenir.
- Gemler boyutu 0 olarak spawnlanır ve 5 saniye içinde boyutu 1 olacak şekilde büyür.
- Bir gemin kazandırdığı Gold değeri = (Başlangıç Satış Fiyatı + (scale Birimi * 100))
- Gem satışı, ilgili alana girilirse 0.1 saniye aralıklarla yapılabilmektedir.

## Ekran Görüntüleri

![Ekran Görüntüsü (291)](https://github.com/gokberksenocak/Growing_Gems/assets/102216059/2f31c233-2b40-4989-9f2c-7051214ccb16)
![Ekran Görüntüsü (293)](https://github.com/gokberksenocak/Growing_Gems/assets/102216059/594a614c-d873-48df-83cc-ecb1ee04d4f1)
![Ekran Görüntüsü (294)](https://github.com/gokberksenocak/Growing_Gems/assets/102216059/9dbc0229-0da5-4b05-90cf-71e67e64d983)
