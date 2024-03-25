import pandas as pd
import numpy as np

# Ajout de nouveaux sujets spécifiques en français et en anglais
sujets = [
    ("Coca", "Coke"), ("les tweets de Donald Trump", "Donald Trump's tweets"),
    ("la RDC Congo", "the DRC Congo"), ("le sucre et le diabète", "sugar and diabetes"),
    ("la pollution", "pollution"), ("la santé", "health"), ("la politique", "politics"),
    ("les boissons", "beverages"), ("la technologie", "technology"), ("les loisirs", "leisure"),
    ("le voyage", "travel"), ("la nourriture", "food"), ("le cinéma", "cinema"),
    ("la littérature", "literature"), ("le sport", "sports"),
    ("l'éducation", "education"), ("l'art", "art"), ("l'économie", "economy"),
    ("le travail", "work"), ("l'environnement", "environment")
]

# Élargissement des opinions positives et négatives en français et en anglais
opinions_positives = [
    "est fantastique", "est excellente",
    "m'a impressionné positivement", "est au-dessus de mes attentes",
    "est incroyable", "est louable",
    "est remarquable", "mérite toute notre attention",
    "est un modèle à suivre", "est à la pointe"
]

opinions_negatives = [
    "ne vaut pas la peine", "est décevante",
    "m'a laissé indifférent",
    "est en dessous de mes attentes",
    "est terrible", "laisse beaucoup à désirer",
    "n'atteint pas le but visé",
    "est source de préoccupation", "est un pas en arrière",
    "manque de substance"
]

avis = []
avis_set = set()  # Ensemble pour stocker les avis déjà générés

# Génération d'avis pour chaque sujet en alternant entre français et en anglais
while len(avis_set) < 325 * 4:  # Tant que nous n'avons pas atteint le nombre d'avis désiré
    sujet_index = np.random.randint(0, len(sujets))  # Sélectionne un indice de sujet aléatoire
    sujet_fr, sujet_en = sujets[sujet_index]  # Décompresse le tuple en sujet_fr et sujet_en
    
    opinion_pos = np.random.choice(opinions_positives)
    opinion_pos_fr, opinion_pos_en = opinion_pos, opinion_pos
    
    opinion_neg = np.random.choice(opinions_negatives)
    opinion_neg_fr, opinion_neg_en = opinion_neg, opinion_neg
    
    avis_positif_fr = f"J'ai trouvé que {sujet_fr} {opinion_pos_fr}. Cela montre un grand potentiel."
    avis_negatif_fr = f"À mon avis, {sujet_fr} {opinion_neg_fr}. Il y a certainement de la place pour l'amélioration."
    avis_positif_en = f"I found that {sujet_en} {opinion_pos_en}. It shows great potential."
    avis_negatif_en = f"In my opinion, {sujet_en} {opinion_neg_en}. There's definitely room for improvement."
    
    # Vérifie si l'avis n'est pas déjà dans l'ensemble avant de l'ajouter
    for avis_phrase in [avis_positif_fr, avis_negatif_fr, avis_positif_en, avis_negatif_en]:
        if avis_phrase not in avis_set:
            avis_set.add(avis_phrase)
            avis.append(avis_phrase)

# Mélange des avis pour varier les opinions
np.random.shuffle(avis)

# Création du DataFrame et sauvegarde dans un fichier CSV
df = pd.DataFrame(avis, columns=["Avis"])
df.to_csv("avis_fictifs_diversifies_1300.csv", index=False, encoding='utf-8-sig')

print("Fichier 'avis_fictifs_diversifies_1300.csv' créé avec succès.")
