# app.py
from flask import Flask,request,jsonify
from google.cloud import language_v1
from google.oauth2 import service_account
from flask_cors import CORS

from tensorflow.keras.models import load_model
from tensorflow.keras.preprocessing.sequence import pad_sequences
import pickle
import re
from nltk.stem import SnowballStemmer
from nltk.corpus import stopwords
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









# Charger le modèle pour l'analyse de sentiment
def load_sentiment_model(model_path='best_sent_french_model.h5'):
    model = load_model(model_path, compile=False)
    return model


# Charger le tokenizer pour l'analyse de sentiment

def load_tokenizer(tokenizer_path='tokenizer_french.pickle'):
    with open(tokenizer_path, 'rb') as handle:
        tokenizer = pickle.load(handle)
    return tokenizer
load_sentiment_model()
load_tokenizer()
# Fonction de prétraitement du texte pour l'analyse de sentiment
def preprocess(text):
    stemmer = SnowballStemmer('french')
    french_stopwords = stopwords.words('french')
    text_cleaning_regex = "@\S+|https?:\S+|http?:\S+|[^A-Za-z0-9]+"
    text = re.sub(text_cleaning_regex, " ", str(text).lower().strip())
    tokens = []
    for word in text.split():
        if word not in french_stopwords:
            word_stem = stemmer.stem(word)
            tokens.append(word_stem)
    return " ".join(tokens)


# Préparation du texte pour la prédiction de sentiment
def prepare_text_for_prediction(text, tokenizer):
    preprocessed_text = preprocess(text)
    sequence = tokenizer.texts_to_sequences([preprocessed_text])
    max_length = 15
    padded_sequence = pad_sequences(sequence, maxlen=max_length, padding='post', truncating='post')
    return padded_sequence


# Analyse du sentiment d'une phrase
def analyze_sentimentM(phrase, model, tokenizer):
    prepared_text = prepare_text_for_prediction(phrase, tokenizer)
    prediction = model.predict(prepared_text)
    sentiment = 'positif' if prediction > 0.5 else 'négatif'
    return sentiment


# Charger les phrases à partir du fichier 
@app.route('/analyze_sentiment', methods=['POST'])
def analyze_sentiment_route():
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
        # Charger le modèle et le tokenizer une seule fois
        model = load_sentiment_model()
        tokenizer = load_tokenizer()

        # Prédire les sentiments pour chaque phrase
        sentiments = [analyze_sentimentM(phrase, model, tokenizer) for phrase in filtered_phrases['Avis']]
        sentiments_category = sentiments

        return jsonify({
            "phrases": filtered_phrases['Avis'].tolist(),
            "sentiments": sentiments_category
        })


if __name__ == '__main__':
    app.run(debug=True)


