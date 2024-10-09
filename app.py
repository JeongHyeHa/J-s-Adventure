import os
from flask import Flask, jsonify, request
import mysql.connector

app = Flask(__name__)

# 환경 변수 출력으로 확인
from dotenv import load_dotenv
import os

# .env 파일에서 환경 변수를 로드
load_dotenv()

# 환경 변수 출력
print("Host:", os.getenv('CLEARDB_HOST'))
print("User:", os.getenv('CLEARDB_USERNAME'))
print("Password:", os.getenv('CLEARDB_PASSWORD'))
print("Database:", os.getenv('CLEARDB_DATABASE'))

# ClearDB 연결 - 환경 변수로 정보 가져오기
connection = mysql.connector.connect(
    host=os.environ.get('CLEARDB_HOST'),
    user=os.environ.get('CLEARDB_USERNAME'),
    password=os.environ.get('CLEARDB_PASSWORD'),
    database=os.environ.get('CLEARDB_DATABASE'),
    use_pure=True
)

# 데이터 조회 API
@app.route('/get-users', methods=['GET'])
def get_users():
    try:
        with connection.cursor() as cursor:
            sql = "SELECT * FROM user"  # 테이블 이름에 맞춰 수정
            cursor.execute(sql)
            result = cursor.fetchall()
            return jsonify(result)  # 결과를 JSON 형식으로 반환
    except Exception as e:
        return jsonify({'error': str(e)})

# 데이터 추가 API
# @app.route('/add-user', methods=['POST'])
# def add_user():
#     data = request.json
#     username = data.get('username')
#     email = data.get('email')
#     try:
#         with connection.cursor() as cursor:
#             sql = "INSERT INTO users (username, email) VALUES (%s, %s)"
#             cursor.execute(sql, (username, email))
#             connection.commit()
#             return jsonify({'message': 'User added successfully!'})
#     except Exception as e:
#         return jsonify({'error': str(e)})

if __name__ == '__main__':
    app.run(debug=True)
