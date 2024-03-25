# app.py
from flask import Flask,request,jsonify
from google.cloud import language_v1
from google.oauth2 import service_account
from flask_cors import CORS
import pandas as pd
import openai
import os
from dotenv import load_dotenv
from mes_ia import generate_response ,create_client, analyze_sentiment, load_phrases


app = Flask(__name__)
CORS(app)

openai.api_key = os.getenv('openAi')  

load_dotenv()


# Utilisez la fonction generate_response comme vue pour cette route
app.route('/chatbot', methods=['POST'])(generate_response)




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

@app.route('/analyze_google', methods=['POST'])
def analyze():
    data = request.json
    keyword = data.get('keyword')
    num_sentences_str = data.get('num_sentences')

    # Convertir num_sentences en entier, avec une valeur par défaut de 5 si non spécifié ou invalide
    try:
        num_sentences = int(num_sentences_str)
    except (TypeError, ValueError):
        num_sentences = 5  # Valeur par défaut

    client = create_client()
    df = pd.read_csv('phrases.csv')
    
    if 'Avis' not in df.columns:
        return jsonify({"error": "Le fichier CSV ne contient pas de colonne 'Avis'."}), 400
    
    filtered_phrases = df[df['Avis'].str.contains(keyword, case=False)].head(num_sentences)
    
    if filtered_phrases.empty:
        return jsonify({"message": "Aucune phrase trouvée contenant le mot-clé spécifié."}), 200
    else:
        sentiments = [analyze_sentiment(phrase, client) for phrase in filtered_phrases['Avis']]
        sentiments_category = ['positif' if score > 0 else 'négatif' for score in sentiments]
        
        return jsonify({
            "phrases": filtered_phrases['Avis'].tolist(),
            "sentiments": sentiments_category
        })

if __name__ == '__main__':
    app.run(debug=True)


