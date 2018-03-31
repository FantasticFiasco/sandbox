# Run load test with the following command:
# locust --host=http://localhost:3000

from locust import HttpLocust, TaskSet, task
from common.status import Status

class Behavior(TaskSet):
    tasks = {Status:10}   # 10x times compared to 'index'

    @task
    def index(self):
        self.client.get("/")

class User(HttpLocust):
    task_set = Behavior