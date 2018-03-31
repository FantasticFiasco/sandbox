# Run load test with the following command:
# locust --host=http://localhost:3000 -f ./task_weight.py

from locust import HttpLocust, TaskSet, task

class Behavior(TaskSet):
    @task(3)   # 3x times compared to 'profile'
    def index(self):
        self.client.get("/")

    @task(1)
    def profile(self):
        self.client.get('/profile')

class User(HttpLocust):
    task_set = Behavior
    min_wait = 1    # 1 ms
    max_wait = 50   # 50 ms