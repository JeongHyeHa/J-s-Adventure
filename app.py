import os
from flask import Flask, jsonify, request
import pymysql

app = Flask(__name__)

# ClearDB 연결 - 환경 변수로 정보 가져오기
connection = pymysql.connect(
    host=os.environ.get('CLEARDB_HOST'),
    user=os.environ.get('CLEARDB_USERNAME'),
    password=os.environ.get('CLEARDB_PASSWORD'),
    db=os.environ.get('CLEARDB_DATABASE'),
    charset='utf8mb4',
    cursorclass=pymysql.cursors.DictCursor
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
