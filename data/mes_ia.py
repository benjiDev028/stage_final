# chatbot.py
from flask import request, jsonify
from google.cloud import language_v1
from google.oauth2 import service_account
import os
import pandas as pd
from dotenv import load_dotenv
import openai

async def generate_response():
    data = request.json
    user_input = data.get('userInput', '')

    try:
        response =   await  openai.ChatCompletion.create(
            model="gpt-3.5-turbo",
            messages=[{"role": "user", "content": user_input}]
        )
        return jsonify({'response': response['choices'][0]['message']['content']}), 200
    except Exception as e:
        return jsonify({'error': str(e)}), 500
    

async def create_client():
    credentials_path =  await os.getenv('GOOGLE_APPLICATION_CREDENTIALS').strip('"')
    credentials = service_account.Credentials.from_service_account_file(credentials_path)
    return language_v1.LanguageServiceClient(credentials=credentials)

async def analyze_sentiment(text, client):
    document = language_v1.Document(content=text, type_=language_v1.Document.Type.PLAIN_TEXT)
    sentiment = await  client.analyze_sentiment(request={'document': document}).document_sentiment
    return sentiment.score

def load_phrases(filename):
    return pd.read_csv(filename)
