# Run load test with the following command:
# locust --host=http://localhost:3000 -f ./wait.py

from locust import HttpLocust, TaskSet, task

class Behavior(TaskSet):
    @task
    def index(self):
        self.client.get("/")

    @task
    def profile(self):
        self.client.get('/profile')

class User(HttpLocust):
    task_set = Behavior
    min_wait = 1    # 1 ms
    max_wait = 50   # 50 ms