# Biznes
"Biznesem" są bezpośredni użytkownicy końcowi, czyli osoby zainteresowane tematyką zdrowotną oraz gracze poszukujący logiczno-detektywistycznych gier.

# Domena biznesowa
### Stan obecny
Obecnie docelowi użytkownicy, chcąc poszerzać swoją wiedzę o chorobach, zazwyczaj opierają się na pasywnych metodach nauki – czytają wykłady lub podręczniki, oglądając biologiczno-medyczne filmiki albo nawet korzystać ze sztucznej inteligencji. Jest to proces monotonny, który w ogóle nie uczy szybkiego myślenia, ani łączenia faktów w sytuacjach stresowych. Z kolei fani łamigłówek grają w tradycyjne gry detektywistyczne, w których brakuje medycznego podłoża.

### Przyszły proces
Użytkownicy docelowi z radością przyjęliby rozwiązanie, które zastąpi bierne przyswajanie wiedzy interaktywnym symulatorem zmuszającym do praktycznej dedukcji. Aplikacja dostarczy środowisko, w którym poprzez zarządzanie "czasem klinicznym", analizę wyglądu pacjentów oraz przeprowadzanie wywiadów medycznych, użytkownik utrwali wiadomości diagnostyczne. System ten nie tylko wspomoże zapamiętywanie informacji i przygotuje do podejmowania decyzji pod presją czasu, ale również dostarczy rozrywki graczom poszukującym wyzwań logicznych.

### **Parametry podróży:**

* **Kontekst nr.1:** Użytkownik znajduje się w bibliotece przy swojej uczelni. Ma za 20 min egzamin z medycyny i przez brak zmobilizowania do wyciągnięcia książki, aby się douczyć to wyciąga laptopa oraz odpala grę dla przypomnienia podstaw.
* **Kontekst nr.2:** Użytkownik znajduje się w swoim mieszkaniu. Jest znudzony, większość gier dla niego to monotonia, a żaden z filmików na YT nie daje mu satysfakcji. Dlatego też daje szansę nowej grze, którą przypadkowo znalazł na internecie.
* **Urządzenia:** Laptop / PC z systemem Windows.
* **Główny cel:** Poprawna diagnoza jak największej liczby pacjentów i pobicie własnego rekordu punktowego, chwilowa powtórka podstawowych informacji medycznych.

### **Frustracje użytkownika:**

- **Odwracanie uwagi:** Nagłe powiadomienie z innej aplikacji rozpraszające uwagę w trakcie rozgrywki.
* **Zbyt surowa kara:** Frustracja, gdy jedna pomyłka skraca czas gry o 10 sekund, co kończy sesję zbyt szybko.
* **Nieczytelny UI:** Problem z rozróżnieniem symptomów z wyglądu postaci albo niezbyt przyzwoita oprawa graficzna całej gry.
* **Nielogiczne diagnozy:** Zbyt duża ilość chorób o podobnych symptomach może znacznie utrudnić wybór diagnozy.

---

### **Etapy Podróży:**

| Lp.   | Krok                              | Funkcjonalność                                                                           | Emocje                                                                  |
| :---- | :-------------------------------- | :--------------------------------------------------------------------------------------- | :---------------------------------------------------------------------- |
| **1** | **Uruchomienie gry**              | Ładowanie bazy danych z plików JSON i wyświetlenie menu głównego.                        | Ciekawość, chęć podjęcia wyzwania.                                      |
| **2** | **Przyjęcie pierwszego pacjenta** | System losuje pacjenta oraz chorobę; na ekranie pojawia się licznik „czasu klinicznego”. | Skupienie, lekka presja związana z czasem.                              |
| **3** | **Analiza wstępna**               | Obserwacja wyglądu postaci i wyciąganie pierwszych wniosków.                             | Satysfakcja z dostrzeżenia symptomów.                                   |
| **4** | **Wywiad lekarski**               | Przeczytanie okienka dialogowego z symptomami, na które skarży się pacjent.              | Zaangażowanie i ponownie satysfakcja z otrzymania nowych objawów.       |
| **5** | **Zlecenie badań dodatkowych**    | Wykorzystanie „wspomagaczy” (badań), które mają swój czas odnowienia.                    | Niepewność – czy warto zużyć badanie teraz, czy zachować je na później? |
| **6** | **Praca z dziennikiem**           | Eliminowanie chorób, które nie pasują do zebranych dowodów.                              | Koncentracja przy eliminacji odpowiednich patogenów.                    |
| **7** | **Postawienie diagnozy**          | Wybór ostatecznej diagnozy przed upływem czasu.                                          | Napięcie przed diagnozą.                                                |
| **8** | **Otrzymanie wyniku**             | Walidacja diagnozy: otrzymanie +3 pkt i +5s czasu lub kary -1 pkt i -10s.                | Radość z sukcesu lub frustracja z powodu błędu i straty czasu.          |

# Ograniczenia:

**L-01 Ograniczenie technologiczne**
System musi być tworzony w silniku Unity 6 z użyciem języka C#, zgodnie z założeniami projektu. 

**L-02 Ograniczenie zasobów medycznych**
Symptomy i choroby dostępne w grze będą oparte na ograniczonym zestawie chorób i symptomów zapisanych w bazie danych projektu. 

**L-03 Ograniczenie czasu realizacji projektu**
System musi zostać zaprojektowany i zaimplementowany w czasie trwania semestru akademickiego. 

**L-04 Ograniczenie językowe**
Interfejs systemu będzie dostępny wyłącznie w języku polskim. 

**L-05 Ograniczenie trybu gry**
System nie będzie posiadał trybu multiplayer ani funkcji współpracy między graczami. 

**L-06 Ograniczenie danych wejściowych**
Gracz może wybierać działania tylko z przygotowanej listy opcji (np. badań i pytań), bez możliwości wpisywania własnych. 

**L-07 Ograniczenie grafiki**
Projekt wykorzystuje proste elementy graficzne 2D zamiast 3D, aby uprościć proces implementacji. 

# Wymagania systemowe: 

**S-01 Liczba użytkowników**
System przewidziany jest dla jednego użytkownika jednocześnie (tryb single player). 

**S-02 Przechowywanie danych**
System musi przechowywać dane przypadków medycznych w plikach JSON. 

**S-03 Oprogramowanie**
System musi działać z wykorzystaniem: 
- Unity 6 
- języka C# 
- systemu kontroli wersji Git + GitHub 

**S-04 Operacje systemowe**
System powinien umożliwiać: 
- wczytanie przypadków medycznych przy uruchomieniu gry 
- zapis postępu gracza 
- odczyt zapisanego postępu 

**S-05 Platforma sprzętowa**
System powinien działać na komputerze osobistym z systemem operacyjnym Microsoft Windows w wersji 10 lub nowszej. 
Minimalne wymagania sprzętowe: 
- procesor: dwurdzeniowy 2 GHz lub lepszy 
- pamięć RAM: minimum 4 GB 
- karta graficzna obsługująca DirectX 11 
- miejsce na dysku: minimum 1 GB 
- Wymagania funkcjonalne: 

# Wymagania funkcjonalne:

**F-01 Przyjęcie pacjenta Priorytet: M**
Historyjka: Jako gracz (lekarz) chcę otrzymać pacjenta z opisem objawów, aby móc rozpocząć proces diagnozy. 
Test: Po rozpoczęciu rozgrywki system generuje przypadek pacjenta. 

**F-02 Przeprowadzenie wywiadu Priorytet: M**
Historyjka użytkownika: Jako gracz chcę zadawać pytania pacjentowi, aby uzyskać dodatkowe informacje o objawach. 
Test: Po wybraniu pytania system pokazuje odpowiedź pacjenta. 

**F-03 Wykonywanie badań Priorytet: M**
Historyjka użytkownika: Jako gracz chcę zlecać badania medyczne, aby zawęzić listę możliwych chorób. 
Test: Po wybraniu badania system zwraca wynik badania. 

**F-04 Postawienie diagnozy Priorytet: M**
Historyjka użytkownika: Jako gracz chcę wybrać diagnozę z listy chorób, aby zakończyć proces leczenia pacjenta. 
Test: Po wybraniu diagnozy system sprawdza poprawność i wyświetla wynik. 

**F-05 – System czasu klinicznego Priorytet: M**
Historyjka użytkownika: Jako gracz chcę mieć ograniczony czas na diagnozę, aby rozgrywka była bardziej realistyczna. 
Test: Każda akcja zmniejsza dostępny czas. 

**F-06 System punktów Priorytet: S**
Historyjka użytkownika: Jako gracz chcę otrzymywać punkty za poprawne diagnozy, aby śledzić swoje postępy. 
Test: Po poprawnej diagnozie liczba punktów wzrasta. 

**F-07 – Dziennik medyczny Priorytet: S**
Historyjka użytkownika: Jako gracz chcę zapisywać informacje o objawach w dzienniku, aby łatwiej analizować przypadek. 
Test: Gracz może zapisać i odczytać notatki w trakcie gry. 

**F-08 – System nowych przypadków Priorytet: C**
Historyjka użytkownika: Jako gracz chcę otrzymywać kolejne przypadki pacjentów, aby móc kontynuować rozgrywkę. 
Test: Po zakończeniu jednego przypadku system generuje następny.  

# Wymagania niefunkcjonalne:  

**N-01 Wydajność**
System powinien wczytywać przypadek pacjenta w czasie krótszym niż 2 sekundy. 
Test: Zmierzony czas wczytywania danych. 

**N-02 Niezawodność**
System nie powinien powodować awarii podczas rozgrywki przez leczenia co najmniej 3 przypadków. 
Test: Test długotrwałej rozgrywki. 

**N-03 Użyteczność**
Interfejs użytkownika musi umożliwiać wykonanie każdej akcji w maksymalnie 3 kliknięciach. 
Test: Test użyteczności interfejsu. 

**N-04 Przenośność**
System musi działać na komputerach z Windows 10 i Windows 11. 
Test: Uruchomienie aplikacji na obu systemach. 

**N-05 Bezpieczeństwo danych**
System nie powinien zapisywać żadnych danych osobowych użytkowników. 
Test: Analiza zapisanych danych i logów aplikacji brak danych osobowych użytkowników.

**N-06 Stabilność**
System nie powinien powodować utraty zapisanego postępu. 
Test: Zamknięcie aplikacji w trakcie działania i ponowne uruchomienie(zapisany postęp zostaje zachowany).

**N-07 Czytelność interfejsu**
UI powinno być czytelne na monitorze 1920×1080. 
Test: Uruchomienie aplikacji w rozdzielczości 1920×1080 i weryfikacja czy wszystkie elementy są widoczne.

 # Persony

**Persona 1: Zmęczony Student Medycyny**
**Imię i nazwisko:** Kacper Skoczylas 
**Wiek i rola:** 20 lat, student 2-go roku medycyny w Krakowie 
**Umiejętności:** Biegły w obsłudze komuterów. 
**Cele:** Szuka relaksu i odskoczni od nauki. Chce poćwiczyć dedukcję i szybkie łączenie faktów, ale bez presji o oceny. 
**Frustracje:** Nudne, sztywne symulatory medyczne oraz gry edukacyjne. 
**Zachowania:** Odpala grę przed snem (20-30 minut), żeby odmóżdżyć się po ciężkim dniu na uczelni.

**Persona 2: Sympatyczny Fanatyk Seriali**
**Imię i nazwisko:** Norbert Kabanosik 
**Wiek i rola:** 16 lat, uczeń i zapalony fan doktora House'a 
**Umiejętności:** 
- Doświadczony gracz na PC.
- Wiedza medyczna: w połowie 4. sezonu "Dr. House'a".

**Cele:** Oglądany przez niego serial zainteresował go tematem i chciałby poczuć się jak doktor.
**Frustracje:** Zbyt zaawansowane symulatory medyczne, obecność tocznia na liście chorób, bo przecież wie, że to nigdy nie jest toczeń.
**Zachowania:** Gra głównie wieczorami po pracy, aby się zrelaksować i pospędzać miło czas.
