## Koordináta eltolása

### Paraméterek

- `lat1`: Eredeti koordináta szélességi fok (radián)
- `lon1`: Eredeti koordináta hosszúságigi fok (radián)
- `lat2`: Eltolt koordináta szélességi fok (radián)
- `lon2`: Eltolt koordináta hosszúsági fok (radián)
- `b`: Eltolás irány (radián)
- `d`: Eltolás távolság (méter)
- `R`: Föld gömb sugár (6378100 méter)

### Kiszámítás

`lat2` = arcsin(sin(`lat1`) * cos(`d` / `R`) + cos(`lat1`) * sin(`d` / `R`) * cos(`b`))

`lon2` = `lon1` + arctan2(sin(`b`) * sin(`d` / `R`) * cos(`lat1`), cos(`d` / `R`) - sin(`lat1`) * sin(`lat2`))

## Tangens pont

### Paraméterek

- `eltol(c, b, d)`: [Koordináta eltolás](#koordináta-eltolása)
  - `c`: Eltolandó koordináta (lat, lon)
  - `b`: Eltolás irány (fok)
  - `d`: Eltolás távolság (méter)
- `cv`: Várakozási kör középpont koordináta (lat, lon)
- `ct`: Várakozási kör tangens pont koordináta (lat, lon)
- `bsz`: Szélirány (`g_wind_vel`) (fok)
- `d`: Loiter kör sugár (`WP_LOITER_RAD`) (méter)

### Kiszámítás

`ct` = `eltol`(`cv`, `bsz` - 90, `d`)

**A fokokat át kell váltani radiánba az `eltol()` függvénynek!**

## Leszállási pont

### Paraméterek

- `eltol(c, b, d)`: [Koordináta eltolás](#koordináta-eltolása)
  - `c`: Eltolandó koordináta (lat, lon)
  - `b`: Eltolás irány (fok)
  - `d`: Eltolás távolság (méter)
- `ct`: Várakozási kör tangens pont koordináta (lat, lon)
- `cl`: Leszállási pont koordináta (lat, lon)
- `bsz`: Szélirány (`g_wind_vel`) (fok)
- `d`: Várakozási távolság (`WaitDistance`) (méter)

### Kiszámítás

`cl` = `eltol`(`ct`, `bsz`, `d`)

**A fokokat át kell váltani radiánba az `eltol()` függvénynek!**
