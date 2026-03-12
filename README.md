Tema: Târg de mașini
-

Aplicația este un sistem de evidență digitală conceput pentru a monitoriza activitatea comercială dintr-un târg auto. Aceasta funcționează ca o bază de date interactivă unde fiecare tranzacție este înregistrată cu detalii complete despre mașină, actori (vânzător/cumpărător) și contextul financiar.

Proiectul se bazează pe modelarea entităților folosind clase în C#, respectând principiile programării orientate pe obiecte:
 - Entitatea Mașină: Va conține atributele tehnice (Marcă, Model, An Fabricație, Culoare, Opțiuni).
 - Entitatea Tranzacție: Va acționa ca un "contract" digital, legând o Mașină de un Vânzător și un Cumpărător, adăugând Data și Prețul.
 - Managerul de Date: O clasă centrală care va conține o listă unde se vor stoca toate operațiunile.

În proiect vor exista operații de introducere a datelor, editarea acestora pentru eventualele erori (ex. s-a introdus un preț greșit, numele cumpărătorului greșit) și posibilitatea de a șterge datele introduse.
În aplicație vor fi posibile afișările următoarelor rapoarte:
 - cea mai căutată mașină ca și firmă sau ca model, într-o anumită perioadă;
 - un grafic al prețului pentru un anumit model, pe o anumită perioadă de timp;
 - afișarea tranzacțiilor dintr-o anumită zi.

Aplicația va implementa un algoritm de verificare la fiecare introducere nouă. Dacă sistemul detectează că același nume (vânzător sau cumpărător) apare în două sau mai multe tranzacții diferite în aceeași dată calendaristică, va declanșa un mesaj de avertizare de tip Warning pe ecran pentru a preveni posibile fraude sau erori.


