# C# laboration 3
## Gästbok

I denna laboration skapades en gästbok där man kan skapa inlägg och radera inlägg.
<img width="618" height="328" alt="image" src="https://github.com/user-attachments/assets/a2312c2b-b279-4552-928c-d33d4c7dc295" />
Skapandet av inläggen sker genom att välja alternativ 1 och där anger man namn och meddelande.
Detta sparas sedan som ett objekt i form av klassen Entry, som sedan sparas i en array och skrivs i json format till filen guestbook.txt.

Raderandet genom att man anger alternativ 2, skriver vilket indexnummer man vill radera och sedan enter för att radera den posten. Koden där är snarlik den för att lägga till, förutom att allting sker i motsatt riktning.

Vid start av konsollappen läses filen in, om den finns och om den har något i sig. Är den tom läses den inte in, för att undvika krasch. 

