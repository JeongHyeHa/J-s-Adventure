import os
from flask import Flask, jsonify, request
import mysql.connector

app = Flask(__name__)

# ClearDB 연결 - 환경 변수로 정보 가져오기
connection = mysql.connector.connect(
    # host=os.environ.get('CLEARDB_HOST'),
    # user=os.environ.get('CLEARDB_USERNAME'),
    # password=os.environ.get('CLEARDB_PASSWORD'),
    # database=os.environ.get('CLEARDB_DATABASE'),
    host='us-cluster-east-01.k8s.cleardb.net',  # 로컬 MySQL 서버 주소
    user='b664f22c0e22f7',  # 로컬 MySQL 사용자 이름
    password='cfbde9d8',  # 로컬 MySQL 비밀번호
    database='heroku_9af0bc3ddcd2b1c',  # 데이터베이스 이름
    use_pure=True  # TCP 연결 사용
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
