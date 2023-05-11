# Multi-threading

Ce dépôt vise à comparer des cas d'utilisation avec un seul thread et plusieurs threads.

## Cas d'utilisation :

### Multi-threading inefficace

Au début du programme, nous créons 10 fichiers et insérons plusieurs milliers de caractères aléatoires dans chacun d'eux. Nous le faisons de deux manières : en parallélisant et sans paralléliser. On remarque alors que la version parallélisée prend davantage de temps.

Cela peut être expliqué par le fait que c'est de l'écriture de fichier, et il n'est peut-être pas possible du point de vue de l'OS utilisé d'accéder en parallèle à de l'écriture, même si ce sont des fichiers différents.

### Multi-threading efficace

Ensuite, nous extrayons de ces fichiers des tableaux de 10k caractères chacun, puis nous y appliquons un algorithme de tri. Dans ce cas, nous pouvons remarquer que la parallélisation est efficace.
