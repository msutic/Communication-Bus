# Communication Bus

Communication Bus je softver koji omogućuje korisniku da upravlja bazom podataka.  
Neke od operacija koje korisnik može da izvrši jesu dodavanje, brisanje, izmena i dobavljanje resursa.

## Dizajn
![Dizajn rešenja](https://i.imgur.com/W2eWieB.png)
### Arhitektura rešenja
Rešenje se sastoji od komponenti koje su vitalne za funkcionalnost projekta, a to su:    
<b>1. DataBaseMaker</b>  
<b>2. Client</b>  
<b>3. WebClient</b>  
<b>4. XmlAdapter</b>  
<b>5. CommunicationBus</b>  
<b>6. XmlToDBAdapter</b>  
<b>7. Repository</b>  

<b>DataBase</b>
 predstavlja bazu podataka kojom će korisnik da upravlja.  
Ona ne predstavlja komponentu, već se pomoću komponente Repository uspostavlja komunikacija sa njom  
i vrše se adekvatni upiti nad istom. 

### Opis implementacije
Projekat je izgrađen tako, da većina komponenti sadrži metode koje se nalaze u interfejsima koji se nude drugim komponentama na upotrebu,  
dok su tu prisutne i komponente koje su konzolne aplikacije kao što su Client i DataBaseMaker.  

- <b>Client</b> je konzola koja služi da korisnik unese zahtev koji se šalje serveru. 
  U ovom slučaju, komponenta WebClient ima ulogu servera.  

- <b>WebClient</b> komponenta očekuje na svom ulazu zahtev koji pristiže od Client-a.  
  Njen zadatak jeste da nad pristiglim zahtevom izvrši validaciju formata. Za svaki zahtev se zna kakvog je formata.  
  Ako je validacija uspešna, zahtev se prosleđuje metodi koja je zadužena za konvertovanje zahteva u JSON format. 
  Takođe, potrebno je utvrditi od kojih se sve polja sastoji pristigli zahtev, kako bi se zahtev korektno mogao konvertovati u JSON.  
  Zatim se taj oblik zahteva šalje narednoj komponenti CommunicationBus. 
  Ukoliko zahtev nije pravilno formatiran, zahtev se ne prosleđuje narednoj komponenti, već se formira poruka o grešci  
  koja se prosleđuje korisniku i vraća ga na ponovni unos zahteva.
  Korisniku se vraća `BAD_FORMAT` `5000` i `poruka o uzroku greške` u JSON formatu.  
  Ispravni formati zahteva, kao i moguća polja i vrste zahteva, mogu se naći na dnu dokumenta.  

- <b>CommunicationBus</b> ima za zadatak samo da prosledi dobijeni zahtev narednoj komponenti.  
  Međutim, kako CommunicationBus ne razume JSON format, već isključivo XML, neophodno je konvertovati zahtev iz JSON formata u odgovarajući XML zahtev.  
  Za ovu konverziju je odgovorna komponenta <b>JSONToXMLAdapter</b>.  
  Nakon ove konverzije, CommunicationBus može da rukuje ovim zahtevom u XML formatu i zatim ga šalje komponenti Repository.  
  Kako Repository komponenta ne ume da radi sa XML formatom, već isključivo sa DB formatom,  
  potrebno je izvršiti konverziju upita u DB format, tj. u SQL upit. Konverzija iz XML formata u SQL upit je zadatak
  <b>XmlToDBAdapter</b> komponente.  
  
- <b>Repository</b> komponenta na ulazu prima konvertovani SQL upit. Njen zadatak jeste da izvrši odgovarajući upit nad bazom.  
  Na osnovu rezultata izvšavanja upita nad bazom, Repository formira odgovor koji je u formatu: `STATUS`, `STATUS_CODE`, `PAYLOAD`.  
  `STATUS` polje može biti `SUCCESS`, `BAD_FORMAT` ili `REJECTED`.  
  `STATUS_CODE` sadrži `2000` ukoliko je `SUCCESS`, `5000` u slučaju  ili `3000` u slučaju `REJECTED`.  
  U `PAYLOAD` se nalazi poruka o grešci u slučaju da je zahtev odbijen, odnosno traženi entitet u slučaju uspešno izvršenog upita.  
  
Korisnik treba da zna da li je njegov zahtev doveo do promene entiteta, dodavanja entiteta, brisanja ili čitanja. Zato, potrebno je
pružiti mu feedback u vidu poruke formatirane u JSON.  
Kako bi poruka stigla do korisnika, potrebno je da `odgovor` koji je Repository komponenta formirala na osnovu odgovora od baze,  
prođe kroz sve komponente kroz koje je prolazio i korisnikov zahtev.  
Kako svaka komponenta ima određeni format koji prihvata, implementirane su metode koje služe da konvertuju `odgovor` u odgovarajuće  
formate, kako bi komponente mogle da ga prosleđuju.
Zbog CommunicationBus-a potrebno je pretvoriti odgovor u XML format.  
Da bi CommunicationBus prosledio XML formatirani odgovor WebClient-u potrebno je da ga konvertuje u JSON, jer on razume samo taj oblik.  
Zatim, WebClient šalje pristigli odgovor Client-u i taj odgovor se ispisuje korisniku na konzoli.
  


## Zahtevi
Projekat Communication Bus komunicira sa bazom podataka. Stoga, korisnik mora imati bazu kojom će upravljati.  
Kako ovaj projekat radi sa mySQL bazom, neophodno je da korisnik instalira ovaj sistem za upravljanje bazom podataka.  
Link do instalacije: [Download mySQL](https://www.mysql.com/downloads/)  

Nakon instalacije, potrebno je da korisnik napravi svoju konekciju preko koje će da pristupi bazi(šemi) kojoj pristupa.  

## Način upotrebe
Pre svega, neophodno je obezbediti da baza kojom se upravlja postoji, ukoliko ne postoji, potrebno je kreirati novu.  
Da bi se to postiglo, potrebno je pokrenuti prvo komponentu DataBaseMaker.  
U DataBaseMaker komponenti, korisniku se otvara konzola koja nudi dve opcije: 1. da kreira novu bazu i 2. da pristupi postojećoj.  
U oba slučaja, neophodno je uneti adresu servera, port, ime korisnika, šifru korisnika i ime postojeće odnosno baze koja treba da se kreira.  
Korisnik nakon unosa dobija povratnu informaciju da li je tabela sa entitetima uspesno kreirana. Ukoliko jeste, korisnik može da  
zatvori konzolu i da nastavi sa radom. U slučaju da je kreiranje neuspešno, korisniku se prikazuje poruka  
o grešci i ponovo mu se nude već pomenute opcije.

Nakon uspešnog selektovanja baze, korisnik može da počne sa slanjem zahteva.  
Potrebno je pokrenuti Client konzolnu aplikaciju (F5 ukoliko je Client označen za startni projekat).  
U konzoli se nalaze informacije o selektovanoj bazi i polje `Request input` koje čeka na unos od strane korisnika.  
Primer zahteva bi bio: `GET /korisnici` - ova naredba će izlistati sve korisnike iz tabele 'korisnici'.  
Korisnik od ovog trenutka može da šalje željene zahteve.  

### Formati 
Ovde se nalaze ispravni formati zahteva koje korisnik može da unese.  
Zahtev se sastoji od sledećih ključnih polja: `Verb`, `Noun`, `Query` i `Fields`.  
Verb i Noun polja su obavezna, dok Query i Fields predstavljaju samo dodatne filtere.  

#### Polja u zahtevu moraju biti razdvojena razmakom.  
Dobar primer: `GET /korisnici/1 name='pera;surname='peric' id;name`  
Loš primer: `GET/korisnici/1name='pera;surname='peric' id;name`

##### Verb predstavlja vrstu metoda.  
Mogući metodi:  
 - `GET` - dobavljanje resursa  
 - `POST` - dodavanje resursa  
 - `PATCH` - izmena resursa  
 - `DELETE` - brisanje resursa  
 
##### Noun se sastoji od naziva tabele i id-a  
Format noun-a je: /nazivTabele/id
Naziv tabele je neophodno uneti, dok id ne mora da se unese, sem u nekim situacijama.  
Na primer, u slučaju da želimo da izlistamo sve korisnike iz tabele, poslaćemo zahtev `GET /korisnici` i u tom slučaju nam ne treba id.  
U slučaju kada želimo da dobavimo samo jednog, određenog korisnika iz tabele, dodaćemo i id, jer je id jedinstven.
Takođe, kada brišemo korisnika, možemo uneti id kako bismo obrisali tačno jednog, određenog korisnika, a možemo i da izostavimo id kako bismo  
obrisali sve korisnike iz tabele.
Međutim, id je neophodno uneti prilikom dodavanja korisnika, jer svaki korisnik mora imati svoj id.
Ovo je obezbeđeno u rešenju i POST zahtev neće proći u slučaju da id nije unesen.  

##### Query služi da bi se dodatno filtrirali resursi
Korisnik ima mogućnost da pronađe i izlista svakog korisnika koji se u tabeli nalazi pod određenim imenom i/ili prezimenom.  
Ovo omogućava query polje.
Format query-a je: `name='pera';surname='peric'`  
Ukoliko polja name ili surname ne postoje, korisnik će biti o tome obavešten i zahtev se neće proslediti. 
Treba napomenuti da ukoliko se u, npr. `GET` zahtevu navede query i u noun-u navede id, korisniku će se vratiti samo onaj korisnik  
koji ispunjava query uslove i pritom ima navedeni id.  

##### Fields polje se koristi za prikaz
Korisnik može da bira koja polja će mu se izlistati kada mu se, na primer vrati lista korisnika.  
Može da izabere da, ako svaki korisnik ima id, ime i prezime, mu se prikažu samo imena svih tih korisnika, bez id-eva i prezimena.  
Format fields polja: `id;name;surname`
Ukoliko neko polje ne postoji, a korisnik ga je naveo u fields-u, dobiće poruku o grešci i zahtev se neće proslediti.  


### Primeri zahteva    
`GET /korisnici/1` - vraća korisnika iz tabele kom je id = 1  
`GET /korisnici name='pera'` - vraća listu svih korisnika koji se zovu pera  
`GET /korisnici surname='petrovic' id` - vraća listu korisnika koji se prezivaju petrovic i prikazuje samo njihone id-eve  
`POST /korisnici/1 name='marko';surname='markovic'` - dodaje korisnika sa imenom marko i prezimenom markovic sa id-em 1 u tabelu  
`PATCH /korisnici/3 name='mitar'` - menja korisniku sa id-em 3 ime i postavlja ga na mitar  
`DELETE /korisnici/5` - briše korisnika iz tabele kom je id = 5  
`DELETE /korisnici name='marko'` - briše sve korisnike iz tabele koji se zovu marko


