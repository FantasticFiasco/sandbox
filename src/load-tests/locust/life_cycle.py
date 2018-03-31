# Run load test with the following command:
# locust --host=http://localhost:3000 -f ./life_cycle.py

from locust import HttpLocust, TaskSet, task

class Behavior(TaskSet):
    def setup(self):
        # Only run before tasks start running
        pass

    def teardown(self):
        # Only run after all tasks have finished and Locust is exiting
        pass

    def on_start(self):
        # Run when simulated user starts executing that TaskSet class
        pass

    def on_stop(self):
        # Run when the TaskSet is stopped
        pass

    @task
    def dummy(self):
        pass

class User(HttpLocust):
    task_set = Behavior

    def setup(self):
        # Only run before tasks start running
        pass

    def teardown(self):
        # Only run after all tasks have finished and Locust is exiting
        pass