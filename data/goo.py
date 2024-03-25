import streamlit as st
import pandas as pd
from google.cloud import language_v1
from google.oauth2 import service_account
from dotenv import load_dotenv
import os
import matplotlib.pyplot as plt

# Chargement des variables d'environnement
load_dotenv()

# Fonction pour créer un client pour l'API Google Language
def create_client():
    credentials_path = os.getenv('GOOGLE_APPLICATION_CREDENTIALS').strip('"')
    credentials = service_account.Credentials.from_service_account_file(credentials_path)
    return language_v1.LanguageServiceClient(credentials=credentials)

# Analyse du sentiment d'une phrase
def analyze_sentiment(text, client):
    document = language_v1.Document(content=text, type_=language_v1.Document.Type.PLAIN_TEXT)
    sentiment = client.analyze_sentiment(request={'document': document}).document_sentiment
    return sentiment.score

# Interface utilisateur
st.title("Analyse de Sentiment basée sur un Mot-clé")
keyword = st.text_input("Entrez le mot-clé", "produit")
num_sentences = st.number_input("Nombre de phrases à analyser", min_value=1, value=5)

if st.button('Analyser'):
    client = create_client()
    df = pd.read_csv('phrases.csv')
    
    # S'assurer que 'Avis' est une colonne dans df
    if 'Avis' not in df.columns:
        st.error("Le fichier CSV ne contient pas de colonne 'Avis'.")
        st.stop()
    
    filtered_phrases = df[df['Avis'].str.contains(keyword, case=False)].head(num_sentences)
    
    if filtered_phrases.empty:
        st.write("Aucune phrase trouvée contenant le mot-clé spécifié.")
    else:
        # Continuer si des phrases correspondantes ont été trouvées
        sentiments = [analyze_sentiment(phrase, client) for phrase in filtered_phrases['Avis']]
        sentiments_category = ['positif' if score > 0 else 'négatif' for score in sentiments]
        
        # Afficher les résultats
        for i, phrase in enumerate(filtered_phrases['Avis']):
            st.write(f"Avis: {phrase} - Sentiment: {sentiments_category[i]}")
        
        positive_count = sentiments_category.count('positif')
        negative_count = sentiments_category.count('négatif')
        if positive_count == 0 and negative_count == 0:
            st.write("Impossible de calculer les sentiments. Aucune donnée valide.")
        else:
            # Préparation des données pour les graphiques
            labels = ['Positif', 'Négatif']
            sizes = [positive_count, negative_count]
            colors = ['lightgreen', 'lightcoral']
            
            fig, (ax1, ax2) = plt.subplots(1, 2, figsize=(12, 6))
            ax1.bar(labels, sizes, color=colors)
            ax1.set_title('Histogramme des Sentiments')
            
            ax2.pie(sizes, labels=labels, colors=colors, autopct='%1.1f%%', startangle=140)
            ax2.axis('equal')  # Assure que le pie chart est rond
            ax2.set_title('Distribution des Sentiments')
            
            st.pyplot(fig)
