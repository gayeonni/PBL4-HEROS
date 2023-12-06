# -*- encoding: euc-kr-*-
import xgboost as xgb
import pymysql
import pandas as pd
from datetime import datetime, timezone, timedelta

conn = pymysql.connect(host='127.0.0.1', user='pbl_guest',passwd='ghkdlxld1042~!' , db='pbl4', charset='utf8')
cur = conn.cursor()

query = f"SELECT * FROM count_people ORDER BY _id DESC LIMIT 1"


xg_filename ="C:/Users/ryeon/Downloads/pbl_xg_model_1128"
check_file = []


def forecasting_people(month, dayofweek, day, hour, min):
    
    forecast_data = pd.DataFrame({
        'month':[month],
        'dayorweek':[dayofweek],
        'day':[day],
        'hour': [hour],
        'min':[min]
        })
    
    model = xgb.Booster(model_file=xg_filename)
    result = model.predict(xgb.DMatrix(forecast_data))
    
    return result

def probability(h, f, c):
    
    nume = (h/f) * 100 #사고 확률을 알고 싶은 시점의 사람 수를 공간을 최대 수용 인원 수로 나눈 후 100을 곱함 
    deno = ((c*400)/f) * 100 # 경찰의 수와 사고가 나지 않도록 경찰이 통제할 수 있는 인원수 400을 곱한 후, 공간의 최대 인원수로 나눔 
    
    return (nume / deno) * 100


# t: 사고 확률을 알고 싶은 시점
# flag: 지하철 무정차 여부 
def predicting(t, flag): 
    i = 0
    people = 0 
    korea_timezone = timezone(timedelta(hours=9))
    now_time = datetime.now(korea_timezone).strftime("%Y/%m/%d %H:%M") #현재 시간을 YYYY-MM-DD HH:MM 형식으로 가져옴
    now_time_dt = pd.to_datetime(now_time) #datetime 형식으로 변경 
    hour = (now_time_dt.hour)

    while i < t: #사고 확률을 알고 싶은 시점까지 반복
        
        if i == 0: 
            cur.execute(query)
            people += cur.fetchone()[2] # 데이터 베이스에 저장된 현재 사람 수를 h변수에 저장 
            i+=1
            
        else:
        
            month = (now_time_dt.month)
            dayofweek = (now_time_dt.dayofweek) 
            day = (now_time_dt.day) 
            hour = (now_time_dt.hour) + i
            mini = (now_time_dt.minute) 
  
            #hour+i 시간의 사람 수를 예측 - 모델이 예측하는데 필요한 정보를 매개변수로 전달 
            #forecasting_people 함수의 반환 값과 flag를 곱한 값을 people에 덧셈 
            people += (forecasting_people(month, dayofweek, day, hour, mini) * flag)
            
            prob = probability(people, 960, 10)
            
            print('time:',hour, 'people:', people, 'prob:', prob)
            
            i+=1 #다음 시점의 사람 수를 예측하기 위해 i에 1을 더함 
                  
    

def main():
    """Main function."""
    
    predicting(6, 1)
 
    
    
if __name__ == '__main__':
    
    #opt = parse_opt()
    main()
    cur.close()


